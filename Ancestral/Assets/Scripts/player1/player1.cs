using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player1 : MonoBehaviour
{
    CharacterController controller;
    Vector3 speedboton;
    float distansFloor = 0.08f;

    public float speed = 15f;
    public float mouseSensitivity = 100f;
    public float xRotationCamera = 0f;
    public float gravity = -10f;

    public Transform onTheFloor;
    public Transform cameraTransForm;

   
    public LayerMask maskFloor;
    public bool isOnTheFloor;

    // Start is called before the first frame update
    void Start()
    {
        //Ocultar y bloquear el cursor para que no salga de la ventana del juego
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isOnTheFloor = Physics.CheckSphere(onTheFloor.position, distansFloor, maskFloor);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis ("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis ("Mouse Y") * mouseSensitivity * Time.deltaTime;

        Vector3 move = transform.TransformDirection(new Vector3(horizontal, 0, vertical).normalized);

        if (isOnTheFloor)
        {
          speedboton.y = -5 * Time.deltaTime;
        }
        else
        {
          speedboton.y += gravity * Time.deltaTime;
        }

        controller.Move(move * speed * Time.deltaTime + speedboton * Time.deltaTime);

        xRotationCamera -= mouseY;
        xRotationCamera = Math.Clamp(xRotationCamera, -90f, 90f);
        cameraTransForm.localRotation = Quaternion.Euler(xRotationCamera, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
}
