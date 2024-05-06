using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public FloatingJoystick joystickMove;
    public float _moveSpeed; 
    public float _moveSpeedStatic;

    public CharacterController controller;

    public float _currentSpeed;
    public Vector3 _moveVector;

    public Transform Ground;
    [SerializeField] private LayerMask layerMask;
    public bool isGround;
    public float GroundDistance;

    private float gravity = -9.81f;
    private Vector3 velocity;

    public bool Pressed;
    private float jumpheight = 0.25f;

    public ParticleSystem ripple;
    
    private bool inWater;

    public Renderer renderer;
    public Shader redShader;
    public GameObject vfxKill;
    private Material[] material;
    
    private Shader originalShader;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        //Ocultar y bloquear el cursor para que no salga de la ventana del juego
        // Cursor.lockState = CursorLockMode.Locked;
        
        material = renderer.materials;

        // Guarda el shader original
        originalShader = material[1].shader;
        print("Shader" + material );       
    }

    // Update is called once per frame
    void Update()
    {
        OnMovePlayer(joystickMove);
        Gravity();

        //RippleCamera.transform.position = transform.position + Vector3.up * 10;
        if (ripple != null) {
            if (isGrounded()) ripple.transform.position = transform.position + transform.forward;
            else ripple.transform.position = transform.position;
            Shader.SetGlobalVector("_Player", transform.position);
        }
    }

    private void OnMovePlayer(FloatingJoystick joystickMove)
    {
        
        float horizontal = Input.GetAxis("Horizontal") + joystickMove.Horizontal ;
        float vertical = Input.GetAxis("Vertical") + joystickMove.Vertical; 

       // _moveVector = Vector3.zero;
        _moveVector = transform.right * horizontal + transform.forward * vertical;
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

        if (ripple != null)
        {
            if (inWater) ripple.gameObject.SetActive(true);
            else ripple.gameObject.SetActive(false);
            Physics.Raycast(transform.position, Vector3.down, 2.7f, LayerMask.GetMask("Ground"));
            Debug.DrawRay(transform.position, Vector3.down * 2.7f);

            //
            float height = controller.height + controller.radius;
            inWater = Physics.Raycast(transform.position + Vector3.up * height, Vector3.down, height * 2, LayerMask.GetMask("Water"));
            Debug.DrawRay(transform.position + Vector3.up * height, Vector3.down * height);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("*************** Material: " + material[1].name);
        print("*************** Shader: " + redShader.name);
        if (other.CompareTag("criminal")) {
            vfxKill.SetActive(true);
            material[1].shader = redShader;
        }

        
        if (ripple != null)
        {
            if (other.gameObject.layer == 4)
            {
                ripple.Emit(transform.position, Vector3.zero, 5, 0.1f, Color.white);
            }
        }


    }


    private void OnTriggerExit(Collider other)
    {
        if (ripple != null) { 
             if (other.gameObject.layer == 4)
                {
                  ripple.Emit(transform.position, Vector3.zero, 5, 0.1f, Color.white);
             }

        }

        if (other.CompareTag("criminal"))
        {
            vfxKill.SetActive(false);
            material[1].shader = originalShader;
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
        }else
        {
            velocity.y += gravity * Time.deltaTime;
        }


        if (isGrounded() && Pressed)
        {
             float jump = jumpheight * -2f * gravity;
             velocity.y = Mathf.Sqrt(jump);
             velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * _moveSpeed * Time.deltaTime);
    }

    public bool isGrounded()
    {
        isGround = Physics.CheckSphere(Ground.position, GroundDistance, layerMask); 

        if (isGround) return true;
        return false;
    }
}
