using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, GameObjectController
{
    public float speed;
    public float health = 100;
    public TMP_Text healthUI;
    public float RaycastDown;
    public float jumpForce;
    public Image damageOverlay;

    private Transform cameraTransform;
    private Rigidbody rb;
    private Camera cam;
    private CameraController camControl;
    private GameController gameController;
    private AudioSource audio;
    private Animation camAnim;

    private void Start()
    {
        InitializeComponents();
    }

    private void FixedUpdate()
    {
        HandleMovementInput();
    }

    private void Update()
    {
        UpdateUI();

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Jump();
        }

        CheckHealth();
        updateDamageOverlay();
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Barrel"))
        {
            gameController.gameOver();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        damageOverlay.color = new Color(1f, 0, 0, 0.33f);

    }

    private void InitializeComponents()
    {
        cam = Camera.main;
        cameraTransform = cam.transform;
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        camControl = cam.GetComponent<CameraController>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        camAnim = cam.GetComponent<Animation>();
    }

    private void HandleMovementInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if ((horizontalInput != 0 || verticalInput != 0) && IsGrounded())
        {
            camAnim.Play();
            
        }
        else
        {
            camAnim.Stop();
            
        }

        Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = (cameraForward * verticalInput + cameraTransform.right * horizontalInput) * speed * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + movement);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void UpdateUI()
    {
        healthUI.text = health.ToString();
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, RaycastDown);
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            gameController.gameOver();
        }
    }
    private void updateDamageOverlay(){
        if ((damageOverlay.color.a > 0))
            damageOverlay.color = new Color(1f, 0, 0, damageOverlay.color.a - 0.001f);
    }
}
