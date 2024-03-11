using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class player1 : MonoBehaviour
{
    CharacterController controller;
    Vector3 speedboton;

    public FloatingJoystick joystickMove;
    public FixedJoystick joystickRotate;
    float distansFloor = 0.08f;

    public float speed = 15f;
    public float mouseSensitivity = 100f;
    public float xRotationCamera = 0f;
    public float gravity = -9.81f;

    public Transform onTheFloor;
    public Transform cameraTransForm;

   
    public LayerMask maskFloor;
    public bool isOnTheFloor;
    public bool isMouseActive;

    // Start is called before the first frame update
    void Start()
    {
        //Ocultar y bloquear el cursor para que no salga de la ventana del juego
       // Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        OnMovePlayer(joystickMove);
        onMoveView();
    }

    private void OnMovePlayer(FloatingJoystick joystickMove)
    {
        isOnTheFloor = Physics.CheckSphere(onTheFloor.position, distansFloor, maskFloor);

        float horizontal = Input.GetAxis("Horizontal");

        float vertical = Input.GetAxis("Vertical");


        Vector3 move = transform.TransformDirection(new Vector3(horizontal + joystickMove.Horizontal, 0, vertical + joystickMove.Vertical).normalized);

        if (isOnTheFloor)
        {

        }

        speedboton.y += gravity * Time.deltaTime;

        controller.Move(move * speed * Time.deltaTime + speedboton * Time.deltaTime);

    }


    private void onMoveView()
    {
        float mouseY = 0;
        float mouseX = 0;

        if (isMouseActive)
        {
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        }
        if (joystickRotate.isActiveAndEnabled)
        {
            mouseY = joystickRotate.Vertical * Time.deltaTime * mouseSensitivity;
            mouseX = joystickRotate.Horizontal * Time.deltaTime * mouseSensitivity;
        }

        xRotationCamera -= mouseY;
        xRotationCamera = Math.Clamp(xRotationCamera, -90f, 90f);
        cameraTransForm.localRotation = Quaternion.Euler(xRotationCamera, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
