using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Lava");
        if (other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().health = 0;
        }
    }
}
