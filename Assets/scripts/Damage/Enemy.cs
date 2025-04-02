using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))] //tohle zajist�, �e objekt v�dy obsahuje komponentu EdgeCollider2D
public class Enemy : MonoBehaviour
{
    public List<Transform> points; // body mezi kter�mi se nep��tel pohybuje
    public int nextID=0; //dal�� bod v seznamu
    int idChengeValue = 1; //ur�uje sm�r pohybu mezi body
    public float speed = 2; //rychlost pohybu

    private void Reset() //metoda volan� p�i restartu
    {
        Init();
    }
    private void Init()
    {
        GetComponent<BoxCollider2D>().isTrigger = true; //nastav� BoxCollider2D jako trigger

        GameObject root = new GameObject(name + "_Root"); //vytvo�� nov� pr�zdn� objekt, kter� bude slou�it pro organizaci ve sc�n�


        transform.SetParent(root.transform); //p�id� nep�itele do t�hle skupiny

        GameObject waypoints = new GameObject("Waypoints"); //Vytvo�� objekt pro ulo�en� bod� po kter�ch se nep��tel h�be

        waypoints.transform.SetParent(root.transform); //p�id� ho do stejn� skupiny jako nep��tele
        waypoints.transform.position = root.transform.position; //um�st� ho na stejn� m�sto jako hlavn� objekt

        GameObject p1 = new GameObject("Point1"); //vytvo�en� prvn�ho bodu pohybu
        p1.transform.SetParent(waypoints.transform); // ulo�� ho do sekce bod� pohybu
        p1.transform.position = root.transform.position; //um�st� bod na m�sto nep��tele

        GameObject p2 = new GameObject("Point1"); 
        p2.transform.SetParent(waypoints.transform);
        p2.transform.position = root.transform.position;

        points = new List<Transform>(); //seznam bod�
        points.Add(p1.transform); //p�id� prvn� bod seznamu
        points.Add (p2.transform); //p�id� druh� bod seznamu
    }

    private void Update()
    {
        MoveToNextPoint(); //zavol� metodu pro pohyb nep��tele
    }

    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextID]; //vybere c�lov� bod z pole
        if (goalPoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-5, 5, 5); // Pokud je c�lov� bod vpravo od nep��tele tak se oto�� sm�rem dopravo
        }
        else
        {
            transform.localScale = new Vector3(5, 5, 5); // pokud je c�lov� bod vlevo od nep��tele tak se oto�� sm�rem doleva
        }

        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime); //posune nep��tele sm�rem k c�lov�mu bodu danou rychlost

        if (Vector2.Distance(transform.position, goalPoint.position)<1f)
        {
            if(nextID == points.Count-1)
            {
                idChengeValue = -1; // pokud je v posledn�m bodu seznamu zm�n� sm�r pohybu zp�t
            }
            if (nextID == 0)
            {
                idChengeValue = 1; // pokud je v prvn�m bodu seznamu zm�n� sm�r pohybu vp�ed
            }
            nextID += idChengeValue; //Posune index na dal�� bod podle sm�ru pohybu
        }
    }
}
