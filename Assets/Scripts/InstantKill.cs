using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantKill : MonoBehaviour, Pickup
{
    private GameObject[] guns;
    public float rotationSpeed = 30f;
    public float floatSpeed = 0.1f;
    public float floatHeight = 0.5f;
    public GameObject collectNoise;
    void Start()
    {
        Inventory Inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        guns = Inventory.inventory;
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
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].GetComponent<Gun>().InstantKill();
        }
        Instantiate(collectNoise);
        Destroy(gameObject);

    }
    
}
