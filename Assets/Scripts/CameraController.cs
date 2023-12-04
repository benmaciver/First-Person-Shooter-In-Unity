using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public Transform playerTransform;

    void Update()
    {
        UpdatePosition();

        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the character on only the y-axis
        transform.Rotate(Vector3.up, mouseX * rotationSpeed);

        // Rotate the camera on the x-axis
        RotateCamera(mouseY);
    }

    void UpdatePosition()
    {
        transform.position = playerTransform.position + new Vector3(0, 1, 0);
    }

    void RotateCamera(float mouseY)
    {
        // Rotate the camera on the x-axis
        transform.Rotate(Vector3.left, mouseY * rotationSpeed);
        // Clamp the x rotation to prevent strange behavior
        ClampXRotation();
    }

    void ClampXRotation()
    {
        float currentXRotation = transform.rotation.eulerAngles.x;
        currentXRotation = Mathf.Clamp(currentXRotation, -360f, 360f);
        transform.rotation = Quaternion.Euler(currentXRotation, transform.rotation.eulerAngles.y, 0f);
    }
}
