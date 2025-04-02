using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public Image[] lives;
    public int livesRemaining;
    //3 životy - 3 ikony (0,1,2)
    //2 životy - 2 ikony (0,1,[2])
    //1 životy - 1 ikona (0,[1],[2])
    //0 životy - bez ikony ([0],[1],[2]) PROHRA

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
        //snížení hodnoty livesRemaining
        livesRemaining--;
        //Skrýt jeden z obrázkù života
        lives[livesRemaining].enabled = false;

        //Pokud nám dojdou životy, prohráváme hru.
        if (livesRemaining == 0)
        {
            Debug.Log("Prohrál jsi");
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            LoseLife();
    }
}
