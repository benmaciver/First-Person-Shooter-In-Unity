using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float destroyTime = 3f; // Set the time after which the object should be destroyed

    void Start()
    {
        // Invoke the DestroyObject method after the specified time
        Invoke("DestroyObject", destroyTime);
    }

    void DestroyObject()
    {
        // Destroy the GameObject this script is attached to
        Destroy(gameObject);
    }
}
