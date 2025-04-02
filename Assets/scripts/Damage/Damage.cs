using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    AudioManager audioManager; //Odkaz na AudioManager pro pøehrávání zvukù

    Vector2 CheckpointPos; //Promìnná pro ukládání posledního dosaženého checkpointu

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();  //Najde objekt se štítkem "Audio" a získá z nìj komponentu AudioManager
    }
    private void Start()
    {
        CheckpointPos = transform.position; // Uloží aktuální pozici hráèe jako výchozí checkpoint
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Damage")) //Pokud se hráè srazí s jiným objektem, který má tag "Damage" zavolá metodu Die()
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
        audioManager.PlaySFX(audioManager.death); //Pøehraje zvuk pro smrt
        Respawn(); //Zavolá metodu Respawn()
    }

    void Respawn()
    {
        transform.position = CheckpointPos; //Nastaví pozici hráèe na uložený checkpoint
    }
}
