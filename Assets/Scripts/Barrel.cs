using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    Rigidbody rb;
    public float lifeTime;
    private float spawnTime;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(transform.forward *5f);

    }
    void Update(){

        if ((spawnTime + lifeTime) < Time.time)
        {
            Destroy(gameObject);
        }   
    }
    /*private void OnTriggerEnter(Collision collision)
    {
        if (collision.collider.tag == "Invisible Wall")
            Destroy(gameObject);
    }*/
}
