using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    AudioManager audioManager;

    Vector2 CheckpointPos;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        CheckpointPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Damage"))
        {
            Die();
        }
    }
    public void UpdateCheckpoint(Vector2 pos)
    {
        CheckpointPos = pos;
    }
    void Die()
    {
        audioManager.PlaySFX(audioManager.death);
        Respawn();
    }

    void Respawn()
    {
        transform.position = CheckpointPos;
    }
}
