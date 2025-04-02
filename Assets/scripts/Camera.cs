using UnityEngine;

public class Camera : MonoBehaviour
{
    internal static UnityEngine.Camera main; // Statická reference na hlavní kameru
    internal static object current; // Statická promìnná pro aktuální kameru
    public Transform hero; // Odkaz na hrdinu, kterého bude kamera sledovat
    public Vector3 offset; // Posun kamery vùèi hrdinovi
    [Range(1, 10)]
    public float smoothFactor; // Faktor vyhlazení pohybu kamery

    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 heroPosition = hero.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, heroPosition, smoothFactor*Time.fixedDeltaTime);
        transform.position = heroPosition; // Nastaví novou pozici kamery
    }
}
