using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public List<Transform> points; //body mezi kterýma se pohybuje daná platforma
    public Transform platform; 
    int goalPoint = 0; //index námi vytvoøeného bodu kam naše platforma smìøuje
    public float moveSpeed = 2; // Rychlost pohybu platformy

    private void Update()
    {
        MoveToNextPoint(); //Zyvolá funkc, která pohybuje naší platformou
    }

    void MoveToNextPoint()
    {
        platform.position = Vector2.MoveTowards(platform.position, points[goalPoint].position, Time.deltaTime * moveSpeed); //platforma se pohybuje smìrem k cílovému bodu s danou rychlostí
        if(Vector2.Distance(platform.position, points[goalPoint].position) < 0.1f) // Když je platforma blížko cílovému bodu
        {
            if (goalPoint == points.Count - 1) //Když je aktuální bod poslední v seznamu vrátí se na zaèátek
            {
                goalPoint = 0;
            }
            else
            {
                goalPoint++; //jinak se posouvá smìr k dalšímu bodu v seznamu
            }
        }
    }
}
