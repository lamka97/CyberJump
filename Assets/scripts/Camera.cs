using UnityEngine;

public class Camera : MonoBehaviour
{
    internal static UnityEngine.Camera main;
    internal static object current;
    public Transform hero;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;

    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 heroPosition = hero.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, heroPosition, smoothFactor*Time.fixedDeltaTime);
        transform.position = heroPosition;
    }
}
