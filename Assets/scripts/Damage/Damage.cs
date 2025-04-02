using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    AudioManager audioManager; //Odkaz na AudioManager pro p�ehr�v�n� zvuk�

    Vector2 CheckpointPos; //Prom�nn� pro ukl�d�n� posledn�ho dosa�en�ho checkpointu

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();  //Najde objekt se �t�tkem "Audio" a z�sk� z n�j komponentu AudioManager
    }
    private void Start()
    {
        CheckpointPos = transform.position; // Ulo�� aktu�ln� pozici hr��e jako v�choz� checkpoint
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Damage")) //Pokud se hr�� sraz� s jin�m objektem, kter� m� tag "Damage" zavol� metodu Die()
        {
            Die();
        }
    }
    public void UpdateCheckpoint(Vector2 pos) //Metoda pro aktualizaci pozice checkpointu
    {
        CheckpointPos = pos;
    }
    void Die()
    {
        audioManager.PlaySFX(audioManager.death); //P�ehraje zvuk pro smrt
        Respawn(); //Zavol� metodu Respawn()
    }

    void Respawn()
    {
        transform.position = CheckpointPos; //Nastav� pozici hr��e na ulo�en� checkpoint
    }
}
