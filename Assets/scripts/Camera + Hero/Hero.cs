using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class movement : MonoBehaviour
{
    Rigidbody2D rb; // Deklarace prom�nn� pro Rigidbody 2D
    Animator animator;
    [SerializeField] Collider2D StandCollider;
    [SerializeField] Transform GroundCheckCollider; //Transform je transformace objektu v 3D prostoru; kontrola jeslti je hr� na zemi
    [SerializeField] Transform headCheckCollider;
    [SerializeField] LayerMask GroundLayer; // Ur�uje, kter� vrstvy jsou pova�ov�ny za zem

    const float GroundCheckRadius = 0.2f; //polom�r oblasti pro kontrolu zem�
    const float HeadCheckRadius = 0.2f;
    [SerializeField] float speed = 1; // Rychlost pohybu postavy
    [SerializeField] float jumpPower = 500; // S�la skoku
    float horizontalValue;
    float runSpeedModifier = 2f; // �prava rychlosti p�i sprintu
    float crouchSpeedModifier = 0.5f;

    [SerializeField] int totalJumps;
    int availableJumps;

    //bool m� dv� hodnoty 'true' nebo 'false'
    bool isGrounded; // zji�t�n�, jestli je hr�� na zemi
    bool isRunning; //sprint
    bool facingRight = true; //oto�en� postavy
    bool multipleJump; //dvojt� skok
    bool isCrouch; //skr�en�
    bool coyoteJump;

    // Metoda Start() je vol�na automaticky p�i startu
    void Start()
    {
        availableJumps = totalJumps;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    //Metoda Update() je vol�na v ka�d�m framu hry
    void Update()
    { 
        horizontalValue = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftControl)) //Kdy� je lev� shift stla�en� funkce isRunning bude zapnuta
        {
            isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl)) //Kdy� je lev� shift povolen� funkce isRunning bude vypnuta
        {
            isRunning = false;
        }

        if (Input.GetButtonDown("Jump")) //Kdy� je Space stla�en� funkce jump bude true
        {
            Jump();
        }

        //Kdy� stiskneme tla��tko pro skr�en� pan��ek se skr��
        if (Input.GetButtonDown("Crouch"))
            isCrouch = true;

        //Kdy� uvoln�me tla��tko pro skr�en� pan��ek se skr��
        else if (Input.GetButtonUp("Crouch"))
            isCrouch = false;

        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    //Metoda FixedUpdate se pou��v� k aktualizaci logiky hry, podobn� jako metoda Update()
    void FixedUpdate()
    {
        GroundCheck(); // Funkce pro kontrolu, zda je postava na zemi

        Move(horizontalValue, isCrouch); // Funkce pro pohyb postavy
    }

    void GroundCheck()
    {
        bool wasGrounded = isGrounded; //slou�� n�m k tomu aby jsme mohli sk�kat dvakr�t poka�d�
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheckCollider.position, GroundCheckRadius, GroundLayer); // Z�sk�n� v�ech kolider� v oblasti GroundCheckCollider
        if (colliders.Length > 0) //pokud naraz�me aspo� na jeden kolider, tak bude zaznamen�no �e je n� hr�� na zemi
        {
            isGrounded = true;
            if (!wasGrounded)
            {
                availableJumps = totalJumps; // Nastav� po�et dostupn�ch skok� na maxim�ln� hodnotu.
                multipleJump = false; // Zak�e mo�nost dal��ho skoku
                 
            }
            foreach(var c in colliders)
            {
                if (c.tag == "MovingPlatform")
                {
                    transform.parent = c.transform;
                }
            }
        }
        else
        {
            transform.parent = null;
            if (wasGrounded) //Pokud byl hr�� na zemi, spust� se metoda CoyoteJumpDelay kter� ��d� zpo�d�n� pro mo�nost skoku.
            {
                StartCoroutine(CoyoteJumpDelay());
            }
        }
        animator.SetBool("Jump", !isGrounded);
    }

    IEnumerator CoyoteJumpDelay() // Metoda CoyoteJumpDelay implementuje zpo�d�n� pro skok pomoc� asynchronn�ho enumer�toru.
    {
        coyoteJump = true;
        yield return new WaitForSeconds(0.2f); // Tento ��dek vyu��v� yield return k zastaven� b�hu metody CoyoteJumpDelay na zadan� �asov� interval.
        coyoteJump = false;
    }

    void Jump()
    {
        if (isGrounded)   // Pokud je hr�� na zemi provede se skok.
        {
            multipleJump = true; //mo�nost dal��ho skoku se zm�n� na true
            availableJumps--; //sn�� po�et skok� ve vzduchu kter� m�me nastaven�

            rb.velocity = Vector2.up * jumpPower;
            animator.SetBool("Jump", true);
        }
        else //pokud hr�� nen� na zemi provede po�et mo�n�ch skok� kter� jsou je�t� k dispozici
        {
            if(coyoteJump) //pokud je aktivn� coyote jump
            {
                multipleJump = true; //povol� dal�� skok
                availableJumps--; //sn�� po�et dostupn�ch skok�

                rb.velocity = Vector2.up * jumpPower; //s�la skoku
                animator.SetBool("Jump", true); //spu�t�n� animace skoku
            }

            if (multipleJump && availableJumps > 0)
            {
                availableJumps--; 

                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }
        }
    }

    void Move(float dir, bool crouchFlag)
    {
        #region Crouch
        //kdy� jsme skr�en� a pust�me tla��tko na kr�en�
        //zkontrolujeme jestli je n�jak� objekt nad hlavou
        //jestli se zde nach�z� na�e postavi�ka bude nad�le skr�en�
        if (!crouchFlag)
        {
            if(Physics2D.OverlapCircle(headCheckCollider.position,HeadCheckRadius,GroundLayer))
            {
                crouchFlag = true;
            }
        }

        //Kdy� stiskneme tla��tko na skr�en� vypne se collider pro st�n� + animace skr�en�
        //sn�en� rychlosti
        //Kdy� stickneme a zru��me funkci pro skr�en� rychlost postavi�ky se n�m hod� do norm�lu
        //zapnut� collideru pro st�n� + vypnut� animace pro skr�en�
        if (isGrounded)
        {
            StandCollider.enabled = !crouchFlag;
        }

        animator.SetBool("Crouch", crouchFlag);

        #endregion
        #region Move & Run
        float xVal = dir * speed * 100 * Time.fixedDeltaTime; // V�po�et horizont�ln� rychlosti
        if (isRunning) // �prava rychlosti p�i b�hu
        {
            xVal *= runSpeedModifier;
        }
        if(crouchFlag)
        {
            xVal *= crouchSpeedModifier;
        }
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y); // Nastaven� c�lov� rychlosti
        rb.velocity = targetVelocity;

        if (facingRight && dir < 0) //Kdy� se hrdina kouk� do prava a za�neme se h�bat do leva oto�� se n�m charakter do leva 
        {
            facingRight = false;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        }

        else if (!facingRight && dir > 0) //Kdy� se hrdina kouk� do leva a za�neme se h�bat do prava oto�� se n�m charakter do prava
        {
            facingRight = true;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * 1, transform.localScale.y, transform.localScale.z);
        }
        #endregion
        // 0 = idle, 6 = walk, 12 = sprint
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }
}