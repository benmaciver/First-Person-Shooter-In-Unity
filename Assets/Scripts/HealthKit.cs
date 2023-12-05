using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthKit : MonoBehaviour, Pickup
{
    public float rotationSpeed = 30f;
    public float floatSpeed = 0.1f;
    public float floatHeight = 0.5f;
    public GameObject collectNoise;

    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    

    private void Update()
    {
        // Rotate the object around its up axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // Float the object up and down
        //Vector3 floatPosition = new Vector3(transform.position.x, Mathf.Sin(Time.time * floatSpeed) * floatHeight, transform.position.z);
        //transform.position = floatPosition;
    }
    public void OnCollisionEnter(){
        player.health += 100;
        Instantiate(collectNoise);
        Destroy(gameObject);
    }
}
