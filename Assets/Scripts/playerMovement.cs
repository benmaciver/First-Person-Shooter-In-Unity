using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class playerMovement : MonoBehaviour
{
    public float speed = 5f; // Adjust the speed as needed
    public Transform cameraTransform;
    public Quaternion cameraRotation;


    private Rigidbody rb;


    public float RaycastDown;
    private float jumpForce = 5f;
    private AudioSource audio;
    private bool isRunning;
    private Vector3 startPos;
       
    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        startPos = transform.position;


    }


    void Update()
    {
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
            /*
        if (!IsGrounded()
            isRunning = false;
        if (isRunning && !audio.isPlaying )
            audio.Play();
        
        else if (!isRunning && audio.isPlaying)
            audio.Stop();
        */



    }
    bool IsGrounded()
    {
        //placeholder that only works in sample scene
        return transform.position.y <= 1;
    }

}