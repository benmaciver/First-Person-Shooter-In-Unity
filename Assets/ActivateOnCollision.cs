using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnCollision : MonoBehaviour
{
    public GameObject[] entities;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision){
        if (collision.collider.tag == "Player"){
            foreach (GameObject entity in entities){
                entity.SetActive(true);
            }
        }
    }
}
