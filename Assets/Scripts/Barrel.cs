using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    Rigidbody rb;
    public float lifeTime;
    private float timeAlive;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timeAlive = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.forward *5f);
        timeAlive += Time.deltaTime;
        if (timeAlive > lifeTime)
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
