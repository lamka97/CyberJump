using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Damage damage; //Odkaz na script damage, kter� bude pou�it pro spr�vu checkpoint�
    public Transform RespawnPoint; //Transform kter� obsahuje pozici, kam se hr�� vr�t� po smrti

    Collider2D coll;
    AudioManager audioManager; //Prom�nn� pro p��stup k AudioManageru, kter� bude p�ehr�vat zvuky po dosa�en� checkpointu
    private void Awake()
    {
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<Damage>();  //Najde objekt kter� m� tag "Player" a z�sk� jeho komponentu Damage
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); //Najde objekt s tagem "Audio" a z�sk� jeho komponentu AudioManager
        coll = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision) //spust� se jakmile se toho dotkne n�jak� objekt
    {
        if (collision.CompareTag("Player")) //Ov��� jestli objekt m� tag "Player"
        {
            audioManager.PlaySFX(audioManager.checkpoint); //P�ehraje zvuk checkpointu pomoc� AudioManageru
            damage.UpdateCheckpoint(transform.position); //Aktualizuje pozici posledn�ho checkpointu v Damage scriptu
            coll.enabled = false; //Deaktivuje collider aby hr�� checkpoint neaktivoval znovu
        }
    }
}
