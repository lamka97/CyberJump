using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGround : MonoBehaviour
{
    [SerializeField] private float fallDelay = 1f; //prodleva pøed pádem platformy poté se se toho dotkne hráè
    [SerializeField] private float destroyDelay = 2f; //prodleva pøed deaktivací platformy po pádu
    private bool falling = false; //stav který urèí jestli platforma zaèala padat
    [SerializeField] private Rigidbody2D rb; // øídí fyziku objektu
    [SerializeField] private float respawnTime = 3f; //prodleva pøed obnovením platformy
    private Vector3 startPosition; //promìnná pro uložení pùvodní pozice platformy

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (falling) //pokud platforma padá nic se nestane
            return;

        if (collision.transform.tag == "Player") //kontrola jestli se platformy dotkne hráè
        {
            StartCoroutine(StartFall()); //spustí padání platformy a následné znièení platformy
        }
    }

    private void Start()
    {
        startPosition = transform.position; // Uložení pùvodní pozice platformy
    }

    private IEnumerator StartFall()
    {
        falling = true; //nastavení padání na true, aby se padání nespustilo znovu

        yield return new WaitForSeconds(fallDelay); //poèká námi urèenou dobu než platforma zaène padat

        rb.bodyType = RigidbodyType2D.Dynamic; //pøepne platformu na dynamickou a zaène padat podle fyziky

        yield return new WaitForSeconds(destroyDelay); //poèká na úplné spadnutí platformy

        // Skrýt platformu
        gameObject.GetComponent<Collider2D>().enabled = false; //znièí kolizi platformy aby hráè nestál ve vzduchu
        rb.bodyType = RigidbodyType2D.Static; //nastaví platformu na statickou aby se po respawnu nehýbala
        rb.velocity = Vector2.zero; //Zastaví pohyb platformy
        transform.position = startPosition; //vrátí platformu na výchozí pozici

        yield return new WaitForSeconds(respawnTime); //poèká námi nastavený èas než se platforma obnoví

        // Znovu aktivovat platformu
        gameObject.GetComponent<Collider2D>().enabled = true; // aktivace kolize platformy aby na ni mohl hríè opìt skoèit
        falling = false; //reset stavu padání
    }
}
