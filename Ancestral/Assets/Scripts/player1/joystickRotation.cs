using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class JoystickRotation : MonoBehaviour
{
    private float Xmove;
    private float Ymove;
    private float yRotationCamera = 0f;
    [SerializeField] private Transform PlayerBody;


    public Transform cameraTransForm;
    public FixedJoystick joystickRotate;
    public bool isMouseActive;
    public Vector2 LookAxis;
    public float Sensivity = 40f;
   
    void Update()
    {
        onMoveView();
    }

    private void onMoveView()
    {
        if (isMouseActive)
        {
            Xmove = Input.GetAxis("Mouse X") * Sensivity * Time.deltaTime;
            Ymove = Input.GetAxis("Mouse Y") * Sensivity * Time.deltaTime;
        }
        else
        {
            Xmove = joystickRotate.Horizontal * Time.deltaTime * Sensivity;
            Ymove = joystickRotate.Vertical * Time.deltaTime * Sensivity;
        }
        
        yRotationCamera -= Ymove;
        yRotationCamera = Math.Clamp(yRotationCamera, -90f, 90f);
        cameraTransForm.localRotation = Quaternion.Euler(yRotationCamera, 0f, 0f);
        PlayerBody.Rotate(Vector3.up * Xmove);
    }
}
