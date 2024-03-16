using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using TMPro;
using UnityEngine;

public class player1 : MonoBehaviour
{
    public FloatingJoystick joystickMove;
    public float _moveSpeed; 
    public float _moveSpeedStatic;

    public CharacterController controller;

    public float _currentSpeed;
    public Vector3 _moveVector;

    [SerializeField] public float groundYOffset;
    [SerializeField] private LayerMask layerMask;
    

    private float gravity = -9.81f;
    private Vector3 velocity;
    private Vector3 spherePose;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        //Ocultar y bloquear el cursor para que no salga de la ventana del juego
        // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        OnMovePlayer(joystickMove);
        Gravity();
   
    }

    private void OnMovePlayer(FloatingJoystick joystickMove)
    {
        

        float horizontal = Input.GetAxis("Horizontal") + joystickMove.Horizontal ;
        float vertical = Input.GetAxis("Vertical") + joystickMove.Vertical; 

       // _moveVector = Vector3.zero;
        _moveVector = transform.right * horizontal + transform.forward * vertical;
        _moveVector.y = velocity.y;
        _currentSpeed = _moveVector.magnitude;
     
        //_animator.SetFloat("Moving", _currentSpeed);

        //if (_currentSpeed != 0f){
        //    Vector3 direction =  Vector3.RotateTowards( transform.forward, _moveVector, _rotateSpeed * Time.deltaTime, 0.0f);
        //    transform.rotation = Quaternion.LookRotation(direction);
        //    AnimationRun();
        //}
       // else if(_currentSpeed == 0f)
       // {
       //     AnimationIdle();
       // }

        controller.Move(_moveVector.normalized * _moveSpeed * Time.deltaTime);

        if(joystickMove.Vertical <= -0.35f)
        {
            _moveSpeed = _moveSpeed/2;
        }
        else
        {
            _moveSpeed = _moveSpeedStatic;
        }

    }

    public void AnimationRun()
    {
        //_animator.SetBool("Move", true);
    }

    public void AnimationIdle()
    {
        //_animator.SetBool("Move", false);
    }

    public void Gravity()
    {
        if (isGrounded() && velocity.y < 0) {
            velocity.y = -1f;
        }
        else {
            velocity.y += gravity * Time.deltaTime;
        }
    }

    public bool isGrounded()
    {
        if (controller.isGrounded) return true;
        return false;
    }
}
