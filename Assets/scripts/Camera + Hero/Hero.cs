using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class movement : MonoBehaviour
{
    Rigidbody2D rb; // Deklarace promìnné pro Rigidbody 2D
    Animator animator;
    [SerializeField] Collider2D StandCollider;
    [SerializeField] Transform GroundCheckCollider; //Transform je transformace objektu v 3D prostoru; kontrola jeslti je hráš na zemi
    [SerializeField] Transform headCheckCollider;
    [SerializeField] LayerMask GroundLayer; // Urèuje, které vrstvy jsou považovány za zem

    const float GroundCheckRadius = 0.2f; //polomìr oblasti pro kontrolu zemì
    const float HeadCheckRadius = 0.2f;
    [SerializeField] float speed = 1; // Rychlost pohybu postavy
    [SerializeField] float jumpPower = 500; // Síla skoku
    float horizontalValue;
    float runSpeedModifier = 2f; // Úprava rychlosti pøi sprintu
    float crouchSpeedModifier = 0.5f;

    [SerializeField] int totalJumps;
    int availableJumps;

    //bool má dvì hodnoty 'true' nebo 'false'
    bool isGrounded; // zjištìní, jestli je hráè na zemi
    bool isRunning; //sprint
    bool facingRight = true; //otoèení postavy
    bool multipleJump; //dvojtý skok
    bool isCrouch; //skrèení
    bool coyoteJump;

    // Metoda Start() je volána automaticky pøi startu
    void Start()
    {
        availableJumps = totalJumps;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    //Metoda Update() je volána v každém framu hry
    void Update()
    { 
        horizontalValue = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.LeftControl)) //Když je levý shift stlaèený funkce isRunning bude zapnuta
        {
            isRunning = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl)) //Když je levý shift povolený funkce isRunning bude vypnuta
        {
            isRunning = false;
        }

        if (Input.GetButtonDown("Jump")) //Když je Space stlaèený funkce jump bude true
        {
            Jump();
        }

        //Když stiskneme tlaèítko pro skrèení panáèek se skrèí
        if (Input.GetButtonDown("Crouch"))
            isCrouch = true;

        //Když uvolníme tlaèítko pro skrèení panáèek se skrèí
        else if (Input.GetButtonUp("Crouch"))
            isCrouch = false;

        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    //Metoda FixedUpdate se používá k aktualizaci logiky hry, podobnì jako metoda Update()
    void FixedUpdate()
    {
        GroundCheck(); // Funkce pro kontrolu, zda je postava na zemi

        Move(horizontalValue, isCrouch); // Funkce pro pohyb postavy
    }

    void GroundCheck()
    {
        bool wasGrounded = isGrounded; //slouží nám k tomu aby jsme mohli skákat dvakrát pokaždé
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheckCollider.position, GroundCheckRadius, GroundLayer); // Získání všech koliderù v oblasti GroundCheckCollider
        if (colliders.Length > 0) //pokud narazíme aspoò na jeden kolider, tak bude zaznamenáno že je náš hráè na zemi
        {
            isGrounded = true;
            if (!wasGrounded)
            {
                availableJumps = totalJumps; // Nastaví poèet dostupných skokù na maximální hodnotu.
                multipleJump = false; // Zakáže možnost dalšího skoku
                 
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
            if (wasGrounded) //Pokud byl hráè na zemi, spustí se metoda CoyoteJumpDelay která øídí zpoždìní pro možnost skoku.
            {
                StartCoroutine(CoyoteJumpDelay());
            }
        }
        animator.SetBool("Jump", !isGrounded);
    }

    IEnumerator CoyoteJumpDelay() // Metoda CoyoteJumpDelay implementuje zpoždìní pro skok pomocí asynchronního enumerátoru.
    {
        coyoteJump = true;
        yield return new WaitForSeconds(0.2f); // Tento øádek využívá yield return k zastavení bìhu metody CoyoteJumpDelay na zadaný èasový interval.
        coyoteJump = false;
    }

    void Jump()
    {
        if (isGrounded)   // Pokud je hráè na zemi provede se skok.
        {
            multipleJump = true; //možnost dalšího skoku se zmìní na true
            availableJumps--; //sníží poèet skokù ve vzduchu které máme nastavené

            rb.velocity = Vector2.up * jumpPower;
            animator.SetBool("Jump", true);
        }
        else //pokud hráè není na zemi provede poèet možných skokù který jsou ještì k dispozici
        {
            if(coyoteJump) //pokud je aktivní coyote jump
            {
                multipleJump = true; //povolí další skok
                availableJumps--; //sníží poèet dostupných skokù

                rb.velocity = Vector2.up * jumpPower; //síla skoku
                animator.SetBool("Jump", true); //spuštìní animace skoku
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
        //když jsme skrèení a pustíme tlaèítko na krèení
        //zkontrolujeme jestli je nìjaký objekt nad hlavou
        //jestli se zde nachází naše postavièka bude nadále skrèená
        if (!crouchFlag)
        {
            if(Physics2D.OverlapCircle(headCheckCollider.position,HeadCheckRadius,GroundLayer))
            {
                crouchFlag = true;
            }
        }

        //Když stiskneme tlaèítko na skrèení vypne se collider pro stání + animace skrèení
        //snížení rychlosti
        //Když stickneme a zrušíme funkci pro skrèení rychlost postavièky se nám hodí do normálu
        //zapnutí collideru pro stání + vypnutí animace pro skrèení
        if (isGrounded)
        {
            StandCollider.enabled = !crouchFlag;
        }

        animator.SetBool("Crouch", crouchFlag);

        #endregion
        #region Move & Run
        float xVal = dir * speed * 100 * Time.fixedDeltaTime; // Výpoèet horizontální rychlosti
        if (isRunning) // Úprava rychlosti pøi bìhu
        {
            xVal *= runSpeedModifier;
        }
        if(crouchFlag)
        {
            xVal *= crouchSpeedModifier;
        }
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y); // Nastavení cílové rychlosti
        rb.velocity = targetVelocity;

        if (facingRight && dir < 0) //Když se hrdina kouká do prava a zaèneme se hýbat do leva otoèí se nám charakter do leva 
        {
            facingRight = false;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
        }

        else if (!facingRight && dir > 0) //Když se hrdina kouká do leva a zaèneme se hýbat do prava otoèí se nám charakter do prava
        {
            facingRight = true;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * 1, transform.localScale.y, transform.localScale.z);
        }
        #endregion
        // 0 = idle, 6 = walk, 12 = sprint
        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }
}