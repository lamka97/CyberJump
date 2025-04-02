using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public List<Transform> points; //body mezi kter�ma se pohybuje dan� platforma
    public Transform platform; 
    int goalPoint = 0; //index n�mi vytvo�en�ho bodu kam na�e platforma sm��uje
    public float moveSpeed = 2; // Rychlost pohybu platformy

    private void Update()
    {
        MoveToNextPoint(); //Zyvol� funkc, kter� pohybuje na�� platformou
    }

    void MoveToNextPoint()
    {
        platform.position = Vector2.MoveTowards(platform.position, points[goalPoint].position, Time.deltaTime * moveSpeed); //platforma se pohybuje sm�rem k c�lov�mu bodu s danou rychlost�
        if(Vector2.Distance(platform.position, points[goalPoint].position) < 0.1f) // Kdy� je platforma bl�ko c�lov�mu bodu
        {
            if (goalPoint == points.Count - 1) //Kdy� je aktu�ln� bod posledn� v seznamu vr�t� se na za��tek
            {
                goalPoint = 0;
            }
            else
            {
                goalPoint++; //jinak se posouv� sm�r k dal��mu bodu v seznamu
            }
        }
    }
}
