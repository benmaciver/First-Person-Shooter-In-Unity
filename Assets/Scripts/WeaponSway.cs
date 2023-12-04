using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float smooth,swayMultiplier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * swayMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * swayMultiplier;

        Quaternion targetRotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion targetRotationY = Quaternion.AngleAxis(-mouseX, Vector3.up);

        Quaternion targetRotation = targetRotationX * targetRotationY;

        //rotation
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);

    }
}
