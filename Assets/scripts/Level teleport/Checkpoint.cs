using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Damage damage;
    public Transform RespawnPoint;

    Collider2D coll;
    AudioManager audioManager;
    private void Awake()
    {
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<Damage>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        coll = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager.PlaySFX(audioManager.checkpoint);
            damage.UpdateCheckpoint(transform.position);
            coll.enabled = false;
        }
    }
}
