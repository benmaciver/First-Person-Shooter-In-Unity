using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float damage = 25f;  
    public float fireCooldown = 5f;
    public float health= 100;
    private float currentCooldown;
    private GameObject player;
    private Transform playerTransform;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Awake(){
        currentCooldown =5f;
    }
    void Start()
    {
        currentCooldown =5f;
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (health<=0){
            Destroy(gameObject);
        }
        if (currentCooldown<=0){
            Ray ray = new Ray(transform.position, playerTransform.position - transform.position);


            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50f))
            {
                // Check if the ray hit the player
                if (hit.collider.CompareTag("Player") )
                {
                    audioSource.Play();
                    damagePlayer();
                    //Invoke("damagePlayer",audioSource.clip.length);
                    currentCooldown=fireCooldown;
                }
            }
        }
        currentCooldown-=Time.deltaTime;

        
    }
    void damagePlayer(){
        player.GetComponent<playerController>().takeDamage(damage);
    }
    public void takeDamage(float damage){
        health-=damage;
    }
}
