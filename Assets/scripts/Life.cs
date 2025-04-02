using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public Image[] lives;
    public int livesRemaining;
    //3 �ivoty - 3 ikony (0,1,2)
    //2 �ivoty - 2 ikony (0,1,[2])
    //1 �ivoty - 1 ikona (0,[1],[2])
    //0 �ivoty - bez ikony ([0],[1],[2]) PROHRA

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damage"))
        {
            LoseLife();
        }
    }

    public void LoseLife()
    {
        //If no lives remaining do nothing
        if (livesRemaining == 0)
            return;
        //sn�en� hodnoty livesRemaining
        livesRemaining--;
        //Skr�t jeden z obr�zk� �ivota
        lives[livesRemaining].enabled = false;

        //Pokud n�m dojdou �ivoty, prohr�v�me hru.
        if (livesRemaining == 0)
        {
            Debug.Log("Prohr�l jsi");
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            LoseLife();
    }
}
