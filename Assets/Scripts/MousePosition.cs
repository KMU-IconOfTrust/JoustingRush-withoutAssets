using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    Camera cam;
    //Rigidbody rb;


    public float rotateSpeed = 1f;

    public float cameraXRotationLimit = 80f;
    public float cameraYRotationLimit = 180f;

    private Vector3 rotation = Vector3.zero;
    private float cameraXRotation = 0f;
    private float cameraYRotation = 0f;
    private float currentCameraXRotation = 0f;
    private float currentCameraYRotation = 0f;


    void Awake()
    {
        cam = gameObject.GetComponent<Camera>();
       // rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        float yRot = Input.GetAxisRaw("Mouse X");
        float xRot = Input.GetAxisRaw("Mouse Y");
   
        cameraXRotation = xRot * rotateSpeed; //x
        cameraYRotation = - yRot * rotateSpeed; //y

    }

    void FixedUpdate() //Movement Rotation
    {
       PreformRotation();
    }

    void PreformRotation() //X, Y회전
    {
        
        //rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            currentCameraXRotation -= cameraXRotation;
            currentCameraXRotation = Mathf.Clamp(currentCameraXRotation, -cameraXRotationLimit, cameraXRotationLimit);

            currentCameraYRotation -= cameraYRotation;
            currentCameraYRotation = Mathf.Clamp(currentCameraYRotation, -cameraYRotationLimit, cameraYRotationLimit);

            cam.transform.localEulerAngles = new Vector3(currentCameraXRotation, currentCameraYRotation, 0f);
        }
        
    }
}
