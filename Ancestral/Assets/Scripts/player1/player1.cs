using System.Collections;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player1 : MonoBehaviour
{
    public FloatingJoystick joystickMove;
    public float moveSpeed; 
    private float _moveSpeed; 

    public CharacterController controller;

    public float _currentSpeed;
    public Vector3 _moveVector;

    public Transform Ground;
    [SerializeField] private LayerMask layerMask;
    public bool isGround;
    public float GroundDistance;

    private float gravity = -9.81f;
    private Vector3 velocity;

    public bool onPressedJumping;
    public float jumpheight = 0.25f;

    public ParticleSystem ripple;
    
    private bool inWater;

    public Renderer renderer;
    public Shader redShader;
    public Shader goldShader;
    public GameObject vfxKill;
    public GameObject vfxCoins;
    private Material[] material;
    
    private Shader originalShader;

    public logiBarLife logiBarLife;

    public float damage = 2.0f;


    //variables para la animacion
    public Animator animator;
    private bool isRuning = false;
    private bool isWalking = false;
    private bool isJumping = false;

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

        float horizontalVertical = horizontal + vertical;


        print("****************** Horizontal:"+ horizontalVertical);



        if (jumpPlayer())
        {
            controllerAnimator(vertical, horizontal, false,true);
            return;
        }



        if (vertical != 0 || horizontal != 0)
        {
            controllerAnimator(vertical, horizontal, false, false);

            _moveVector = transform.right * horizontal + transform.forward * vertical;
            _currentSpeed = _moveVector.magnitude;



            controller.Move(_moveVector.normalized * _moveSpeed * Time.deltaTime);

        }
        else {
            controllerAnimator(vertical, horizontal, true, false);
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

    void controllerAnimator(float moveVertical, float moveHorizontal, bool isIdle, bool isPresseJumping) {


        if (isPresseJumping) { 
            isWalking = false;
            isRuning = false;
            isJumping = true;

            setStateAnimator();
            return;
        }

        if (isIdle)
        {
            isWalking = false;
            isRuning = false;
            isJumping = false;

            setStateAnimator();
            return;
        }
        //||
        if ((moveVertical >= -0.5 && moveVertical <= 0.5) && (moveHorizontal >= -0.5 && moveHorizontal <= 0.5))
        {
            isWalking = true;
            isRuning = false;
            isJumping = false;
           
            if (moveVertical >= -0.35f && moveVertical <= -0.35f)
            {
                _moveSpeed = moveSpeed / 5;
            }
            else {
                _moveSpeed = moveSpeed / 3;
            }
        }
        else {
            _moveSpeed = moveSpeed;
            isWalking = false;
            isRuning = true;
            isJumping = false;

        }

        setStateAnimator();
        return;
    }

    void setStateAnimator() {
        animator.SetBool("isRuning", isRuning);
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isJumping", isJumping);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("*************** Material: " + material[1].name);
        print("*************** Shader: " + redShader.name);
        if (other.CompareTag("criminal")) {
            vfxKill.SetActive(true);
            material[1].shader = redShader;
        }

        if (other.CompareTag("coins"))
        {
            vfxCoins.SetActive(true);
            material[1].shader = goldShader;
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

            logiBarLife.lifeNow -= damage;
        }

        if (other.CompareTag("coins"))
        {
            other.gameObject.SetActive(false);
            StartCoroutine(HandleCoin());    
        }
    }

    // Coroutine para manejar el retraso
    IEnumerator HandleCoin()
    {
        // Espera 5 segundos
        yield return new WaitForSeconds(1);

        // Después del retraso, ejecuta tu código
        vfxCoins.SetActive(false);
        material[1].shader = originalShader;
        
    }

    bool jumpPlayer() {

        if (isGrounded() && onPressedJumping)
        {
            float jump = jumpheight * -2f * gravity;
            velocity.y = Mathf.Sqrt(jump);
            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * _moveSpeed * Time.deltaTime);
            return true;
        }
        return false;
    }


    public void Gravity()
    {
        if (isGrounded() && velocity.y < 0) {
            velocity.y = -1f;
        }else
        {
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
