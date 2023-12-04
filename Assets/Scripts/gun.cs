using UnityEngine;
using UnityEngine.UI;

public class Gun : Weapon
{
    public bool isAutomatic;
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
    public float weaponRange = 100f;

    private Camera cam;
    private float currentCooldown;
    private RawImage crosshair;
    private AudioSource audio;
    private GameObject lightInstance;
    private GameObject player;

    private void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        audio = GetComponent<AudioSource>();

        cam = Camera.main;

        Canvas canvas = cam.GetComponentInChildren<Canvas>();
        crosshair = canvas.GetComponentInChildren<RawImage>();

        magSize = magCapacity;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        anim = GetComponent<Animation>();
        AddAnimationClip("Reload", reload);
        AddAnimationClip("Shoot", shoot);

        SetAnimationSpeed("Shoot", shoot.length / FireCooldown);
    }

    private void Update()
    {
        if (!muzzleFlash.isPlaying)
            Destroy(lightInstance);

        if (Input.GetKeyDown(reloadButton))
        {
            Reload();
        }

        if ((!isAutomatic && Input.GetKeyDown(fireButton)) || (isAutomatic && Input.GetKey(fireButton)))
        {
            HandleFireInput();
        }

        currentCooldown -= Time.deltaTime;
        AdjustCrosshairScale();
    }

    private void Reload()
    {
        audio.clip = reloadAudio;
        audio.Play();
        anim.Play("Reload");
        magSize = magCapacity;
    }

    private void HandleFireInput()
    {
        if (currentCooldown <= 0f && magSize > 0 && !anim.isPlaying)
        {
            Fire();
        }
        if (magSize == 0 && currentCooldown <= 0f && !audio.isPlaying)
        {
            audio.clip = emptyAudio;
            audio.Play();
            currentCooldown = FireCooldown;
        }
    }

    private void Fire()
    {
        audio.clip = gunshotAudio;
        audio.Play();
        muzzleFlash.Play();
        lightInstance = Instantiate(light, muzzleFlash.transform.position, muzzleFlash.transform.rotation);
        anim.Play("Shoot");
        Kickback();
        currentCooldown = FireCooldown;
        magSize--;

        Ray ray = cam.ScreenPointToRay(GetRandomPointInCrosshair());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, weaponRange))
        {
            ProcessHit(hit);
        }

        Debug.DrawRay(ray.origin, ray.direction * weaponRange, Color.red, 1f);
    }

    private void Kickback()
    {
        int val = Random.value < 0.5 ? -1 : 1;

        Quaternion targetRotationX = Quaternion.Euler(-(recoil / 100), 0, 0);
        Quaternion targetRotationY = Quaternion.Euler(0, val, 0);

        float smoothness = 0.5f;
        cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, cam.transform.rotation * targetRotationX * targetRotationY, smoothness);

        crosshair.transform.localScale += new Vector3(0.5f, 0.5f);

        if (crosshair.transform.localScale.x > 3f)
            crosshair.transform.localScale = new Vector3(3f, 3f);
    }

    private void AdjustCrosshairScale()
    {
        if (crosshair.transform.localScale.x > 0.5f)
            crosshair.transform.localScale -= new Vector3(0.01f, 0.01f);
    }

    private void ProcessHit(RaycastHit hit)
    {
        if (hit.collider.gameObject.CompareTag("Terrain"))
            Instantiate(bulletImpact, hit.point, Quaternion.LookRotation(hit.normal));
        if (hit.collider.gameObject.CompareTag("Target"))
            Destroy(hit.collider.gameObject);
        if (hit.collider.gameObject.CompareTag("Enemy"))
            hit.collider.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
        if (hit.rigidbody != null)
            hit.rigidbody.AddForce(-hit.normal * impactForce);
    }

    private Vector3 GetRandomPointInCrosshair()
    {
        Vector3 crosshairCenter = crosshair.transform.position;
        Vector3 crosshairSize = crosshair.rectTransform.localScale;

        float randomX = Random.Range(crosshairCenter.x - crosshairSize.x / 2, crosshairCenter.x + crosshairSize.x / 2);
        float randomY = Random.Range(crosshairCenter.y - crosshairSize.y / 2, crosshairCenter.y + crosshairSize.y / 2);

        return new Vector3(randomX, randomY);
    }

    private void SetAnimationSpeed(string clipName, float speed)
    {
        GetComponent<Animation>()[clipName].speed = speed;
    }

    private void AddAnimationClip(string clipName, AnimationClip clip)
    {
        anim.AddClip(clip, clipName);
    }
}
