using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public Transform playerTransform;

    void Update()
    {
        transform.position = playerTransform.position + new Vector3 (0,1,0);

        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X");//Mouse movement on x axis (right == 1)
        float mouseY = Input.GetAxis("Mouse Y");//Same on y axis (up == 1)

        // Rotate the character on only x axis
        transform.Rotate(Vector3.up, mouseX * rotationSpeed);

        // Rotate the camera 
        transform.Rotate(Vector3.left, mouseY * rotationSpeed );
        transform.Rotate(Vector3.up, mouseX * rotationSpeed  );
        //Note for self: Euler angles can represent a three dimensional rotation by performing three separate rotations around individual axes
        // And quaternions are data types that represent rotation in a 3 dimensional space, meaning quaternions and euler are heavily linked 
        float currentXRotation = transform.rotation.eulerAngles.x;//gets curent rotation on the x axis
        currentXRotation = Mathf.Clamp(currentXRotation, -360, 360f); //restricts the x rotation from esxceeding 360 degrees beaause the it starts acting very funky
        transform.rotation = Quaternion.Euler(currentXRotation, transform.rotation.eulerAngles.y, 0f);// sets the cameras rotation but within the clamped limits set



    }
}
