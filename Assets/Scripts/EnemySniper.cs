using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySniper : MonoBehaviour, GameObjectController
{
    public float damage = 25f;  
    public float fireCooldown = 5f;
    public float health= 100;
    public GameObject damageNumber;
    private float currentCooldown;
    private GameObject player;
    private Transform playerTransform;
    private AudioSource audioSource;
    public GameObject[] itemDrops;




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
            DropItem();
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
        player.GetComponent<PlayerController>().TakeDamage(damage);
    }
    public void TakeDamage(float damage){
        if (damageNumber != null)
            ShowFloatingText(damage);
        health-=damage;
    }
    void ShowFloatingText(float damage)
    {
        var go = Instantiate(damageNumber, transform.position, Quaternion.identity);
        go.GetComponent<TextMesh>().text = damage.ToString();

        // Get the direction vector from the text to the player
        Vector3 directionToPlayer = (player.transform.position - go.transform.position).normalized;

    }
    public void DropItem(){
        int chance = Random.Range(0, 100);
        if (chance < 25)
        {
            int random = Random.Range(0, itemDrops.Length);
            Instantiate(itemDrops[random], transform.position, Quaternion.identity);
        }
    }

}
