using UnityEngine;

public class Camera : MonoBehaviour
{
    internal static UnityEngine.Camera main; // Statick� reference na hlavn� kameru
    internal static object current; // Statick� prom�nn� pro aktu�ln� kameru
    public Transform hero; // Odkaz na hrdinu, kter�ho bude kamera sledovat
    public Vector3 offset; // Posun kamery v��i hrdinovi
    [Range(1, 10)]
    public float smoothFactor; // Faktor vyhlazen� pohybu kamery

    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 heroPosition = hero.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, heroPosition, smoothFactor*Time.fixedDeltaTime);
        transform.position = heroPosition; // Nastav� novou pozici kamery
    }
}
