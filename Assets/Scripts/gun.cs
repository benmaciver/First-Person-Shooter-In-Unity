using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class gun : MonoBehaviour
{
    public int magSize;
    public int magCapacity;
    public AnimationClip shoot;
    public AnimationClip reload;
    public AnimationClip sprint;
    public float FireCooldown;
    public KeyCode fireButton;
    public KeyCode reloadButton;
    public float recoil;
    public ParticleSystem muzzleFlash;
    public GameObject bulletImpact;
    public AudioClip gunshotAudio;
    public AudioClip reloadAudio;
    public AudioClip emptyAudio;
    public GameObject light;
    public int impactForce;
    public float damage;


    private Animation anim;
    private Camera cam;
    private float currentCooldown;
    private RawImage crosshair;
    private AudioSource audio;
    private GameObject lightInstance;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audio = GetComponent<AudioSource>();


        cam = Camera.main;
        // Get the child canvas object of the camera.
        Canvas canvas = cam.GetComponentInChildren<Canvas>();

        // Get the RawImage child of the canvas.
        crosshair = canvas.GetComponentInChildren<RawImage>();

        magSize = magCapacity;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        anim = GetComponent<Animation>();
        anim.AddClip(reload, "Reload");
        anim.AddClip(shoot, "Shoot");

    }

    // Update is called once per frame
    void Update()
    {
        if (!muzzleFlash.isPlaying)
            Destroy(lightInstance);

        if (Input.GetKeyDown(reloadButton))
        {
            audio.clip = reloadAudio;
            audio.Play();
            anim.Play("Reload");
            magSize = magCapacity;

        }
        if (Input.GetKey(fireButton))//left mouse button
        {
            if (currentCooldown <= 0f && magSize > 0 && !anim.isPlaying)//cooldown replenished
            {
                fire();
                
            }
            if (magSize == 0 && currentCooldown <=0f&& !audio.isPlaying)
            {
                audio.clip = emptyAudio;
                audio.Play();
                currentCooldown = FireCooldown;
            }
        }
        currentCooldown -= Time.deltaTime;
        if (crosshair.transform.localScale.x > 0.5f)
            crosshair.transform.localScale -= new Vector3(0.01f, 0.01f);

    }

    void fire()
    {
        audio.clip = gunshotAudio;
        audio.Play();
        muzzleFlash.Play();
        lightInstance = Instantiate(light, muzzleFlash.transform.position, muzzleFlash.transform.rotation);
        anim.Play("Shoot");
        kickback();
        currentCooldown = FireCooldown;
        magSize--;
        Ray ray = cam.ScreenPointToRay(GetRandomPointInCrosshair());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Terrain") 
                Instantiate(bulletImpact, hit.point,Quaternion.LookRotation(hit.normal));
            if (hit.collider.gameObject.tag == "Target")
                Destroy(hit.collider.gameObject);
            if (hit.collider.gameObject.tag == "Enemy")
                hit.collider.gameObject.GetComponent<EnemyController>().takeDamage(damage);
            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal* impactForce);//objects stagger on impact

        }
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);

    }

    void kickback()
    {
        int val;
        if (Random.value < 0.5)
            val = -1;
        else val = 1;

        Quaternion targetRotationX = Quaternion.Euler(-(recoil / 100), 0, 0);
        Quaternion targetRotationY = Quaternion.Euler(0, val, 0);

        float smoothness = 0.5f; // You can adjust this value

        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, cam.transform.rotation * targetRotationX * targetRotationY, smoothness);

        crosshair.transform.localScale+=(new Vector3(0.5f,0.5f));

        if (crosshair.transform.localScale.x > 4f)
            crosshair.transform.localScale = new Vector3(5f, 5f);     

    }
    Vector3 GetRandomPointInCrosshair()
    {
        // Get the center and size of the crosshair
        Vector3 crosshairCenter = crosshair.transform.position;
        Vector3 crosshairSize = crosshair.rectTransform.localScale;

        // Generate a random point within the crosshair
        float randomX = Random.Range(crosshairCenter.x - crosshairSize.x / 2, crosshairCenter.x + crosshairSize.x / 2);
        float randomY = Random.Range(crosshairCenter.y - crosshairSize.y / 2, crosshairCenter.y + crosshairSize.y / 2);

        return new Vector3 (randomX, randomY);
    }

    

}
