using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class playerController : MonoBehaviour
{
    public float speed; // Adjust the speed as needed
    public float health =100;
    public TMP_Text healthUI;

    private Transform cameraTransform;
    private Quaternion cameraRotation;
    private Rigidbody rb;
    private Camera cam;
    private CameraController camControl;
    private GameController gameController;

    public float RaycastDown;
    public float jumpForce;
    private AudioSource audio;
    private bool isRunning;
    private Vector3 startPos;
       
    private void Start()
    {
        cam = Camera.main;
        cameraTransform=cam.transform;
        cameraRotation = cameraTransform.rotation;
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        startPos = transform.position;
        camControl = cam.GetComponent<CameraController>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();


    }


    void Update()
    {
        healthUI.text = health.ToString();
        if (startPos != transform.position) 
            isRunning = true;
        else isRunning = false;
        startPos = transform.position;

        // Get input from WASD keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Get the camera's forward direction without vertical component
        Vector3 cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;

        // Calculate movement direction based on camera and input
        Vector3 movement = (cameraForward * verticalInput + cameraTransform.right * horizontalInput) * speed * Time.deltaTime;

        // Apply the movement to the player
        rb.MovePosition(transform.position + movement);

        // Get spacebar input
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (health <=0)
            gameController.gameOver();
        /*
        if (Input.GetKeyDown(KeyCode.LeftShift))
            sprint();
        else if (Input.GetKeyUp(KeyCode.LeftShift)) 
            stopSprint();
            /*
        if (!IsGrounded()
            isRunning = false;
        if (isRunning && !audio.isPlaying )
            audio.Play();
        
        else if (!isRunning && audio.isPlaying)
            audio.Stop();
        */



    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Barrel")
        {
            gameController.gameOver();
        }
    
    }
    public void takeDamage(float damage){
        health -= damage;
    }


}