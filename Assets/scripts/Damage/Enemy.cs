using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))] //tohle zajistí, že objekt vždy obsahuje komponentu EdgeCollider2D
public class Enemy : MonoBehaviour
{
    public List<Transform> points; // body mezi kterými se nepøítel pohybuje
    public int nextID=0; //další bod v seznamu
    int idChengeValue = 1; //urèuje smìr pohybu mezi body
    public float speed = 2; //rychlost pohybu

    private void Reset() //metoda volaná pøi restartu
    {
        Init();
    }
    private void Init()
    {
        GetComponent<BoxCollider2D>().isTrigger = true; //nastaví BoxCollider2D jako trigger

        GameObject root = new GameObject(name + "_Root"); //vytvoøí nový prázdný objekt, který bude sloužit pro organizaci ve scénì


        transform.SetParent(root.transform); //pøidá nepøitele do téhle skupiny

        GameObject waypoints = new GameObject("Waypoints"); //Vytvoøí objekt pro uložení bodù po kterých se nepøítel hýbe

        waypoints.transform.SetParent(root.transform); //pøidá ho do stejné skupiny jako nepøátele
        waypoints.transform.position = root.transform.position; //umístí ho na stejné místo jako hlavní objekt

        GameObject p1 = new GameObject("Point1"); //vytvoøení prvního bodu pohybu
        p1.transform.SetParent(waypoints.transform); // uloží ho do sekce bodù pohybu
        p1.transform.position = root.transform.position; //umístí bod na místo nepøítele

        GameObject p2 = new GameObject("Point1"); 
        p2.transform.SetParent(waypoints.transform);
        p2.transform.position = root.transform.position;

        points = new List<Transform>(); //seznam bodù
        points.Add(p1.transform); //pøidá první bod seznamu
        points.Add (p2.transform); //pøidá druhý bod seznamu
    }

    private void Update()
    {
        MoveToNextPoint(); //zavolá metodu pro pohyb nepøátele
    }

    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextID]; //vybere cílový bod z pole
        if (goalPoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-5, 5, 5); // Pokud je cílový bod vpravo od nepøítele tak se otoèí smìrem dopravo
        }
        else
        {
            transform.localScale = new Vector3(5, 5, 5); // pokud je cílový bod vlevo od nepøítele tak se otoèí smìrem doleva
        }

        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime); //posune nepøítele smìrem k cílovému bodu danou rychlost

        if (Vector2.Distance(transform.position, goalPoint.position)<1f)
        {
            if(nextID == points.Count-1)
            {
                idChengeValue = -1; // pokud je v posledním bodu seznamu zmìní smìr pohybu zpìt
            }
            if (nextID == 0)
            {
                idChengeValue = 1; // pokud je v prvním bodu seznamu zmìní smìr pohybu vpøed
            }
            nextID += idChengeValue; //Posune index na další bod podle smìru pohybu
        }
    }
}
