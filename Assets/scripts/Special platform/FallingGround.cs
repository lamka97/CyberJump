using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGround : MonoBehaviour
{
    [SerializeField] private float fallDelay = 1f; //prodleva p�ed p�dem platformy pot� se se toho dotkne hr��
    [SerializeField] private float destroyDelay = 2f; //prodleva p�ed deaktivac� platformy po p�du
    private bool falling = false; //stav kter� ur�� jestli platforma za�ala padat
    [SerializeField] private Rigidbody2D rb; // ��d� fyziku objektu
    [SerializeField] private float respawnTime = 3f; //prodleva p�ed obnoven�m platformy
    private Vector3 startPosition; //prom�nn� pro ulo�en� p�vodn� pozice platformy

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (falling) //pokud platforma pad� nic se nestane
            return;

        if (collision.transform.tag == "Player") //kontrola jestli se platformy dotkne hr��
        {
            StartCoroutine(StartFall()); //spust� pad�n� platformy a n�sledn� zni�en� platformy
        }
    }

    private void Start()
    {
        startPosition = transform.position; // Ulo�en� p�vodn� pozice platformy
    }

    private IEnumerator StartFall()
    {
        falling = true; //nastaven� pad�n� na true, aby se pad�n� nespustilo znovu

        yield return new WaitForSeconds(fallDelay); //po�k� n�mi ur�enou dobu ne� platforma za�ne padat

        rb.bodyType = RigidbodyType2D.Dynamic; //p�epne platformu na dynamickou a za�ne padat podle fyziky

        yield return new WaitForSeconds(destroyDelay); //po�k� na �pln� spadnut� platformy

        // Skr�t platformu
        gameObject.GetComponent<Collider2D>().enabled = false; //zni�� kolizi platformy aby hr�� nest�l ve vzduchu
        rb.bodyType = RigidbodyType2D.Static; //nastav� platformu na statickou aby se po respawnu neh�bala
        rb.velocity = Vector2.zero; //Zastav� pohyb platformy
        transform.position = startPosition; //vr�t� platformu na v�choz� pozici

        yield return new WaitForSeconds(respawnTime); //po�k� n�mi nastaven� �as ne� se platforma obnov�

        // Znovu aktivovat platformu
        gameObject.GetComponent<Collider2D>().enabled = true; // aktivace kolize platformy aby na ni mohl hr�� op�t sko�it
        falling = false; //reset stavu pad�n�
    }
}
