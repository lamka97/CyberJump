using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Damage damage; //Odkaz na script damage, který bude použit pro správu checkpointù
    public Transform RespawnPoint; //Transform který obsahuje pozici, kam se hráè vrátí po smrti

    Collider2D coll;
    AudioManager audioManager; //Promìnná pro pøístup k AudioManageru, který bude pøehrávat zvuky po dosažení checkpointu
    private void Awake()
    {
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<Damage>();  //Najde objekt který má tag "Player" a získá jeho komponentu Damage
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); //Najde objekt s tagem "Audio" a získá jeho komponentu AudioManager
        coll = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision) //spustí se jakmile se toho dotkne nìjaký objekt
    {
        if (collision.CompareTag("Player")) //Ovìøí jestli objekt má tag "Player"
        {
            audioManager.PlaySFX(audioManager.checkpoint); //Pøehraje zvuk checkpointu pomocí AudioManageru
            damage.UpdateCheckpoint(transform.position); //Aktualizuje pozici posledního checkpointu v Damage scriptu
            coll.enabled = false; //Deaktivuje collider aby hráè checkpoint neaktivoval znovu
        }
    }
}
