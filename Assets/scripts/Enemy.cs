using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class Enemy : MonoBehaviour
{
    public List<Transform> points;
    public int nextID=0;
    int idChengeValue = 1;
    public float speed = 2;

    private void Reset()
    {
        Init();
    }
    private void Init()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;

        GameObject root = new GameObject(name + "_Root");

        transform.SetParent(root.transform);

        GameObject waypoints = new GameObject("Waypoints");

        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;

        GameObject p1 = new GameObject("Point1"); p1.transform.SetParent(waypoints.transform);p1.transform.position = root.transform.position;
        GameObject p2 = new GameObject("Point1"); p2.transform.SetParent(waypoints.transform);p2.transform.position = root.transform.position;

        points = new List<Transform>();
        points.Add(p1.transform);
        points.Add (p2.transform);
    }

    private void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextID];
        if (goalPoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-5, 5, 5);
        }
        else
        {
            transform.localScale = new Vector3(5, 5, 5);
        }

        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, goalPoint.position)<1f)
        {
            if(nextID == points.Count-1)
            {
                idChengeValue = -1;
            }
            if (nextID == 0)
            {
                idChengeValue = 1;
            }
            nextID += idChengeValue;
        }
    }
}
