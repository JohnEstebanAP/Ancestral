using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class ControllerCriminal : MonoBehaviour
{
    public int rutina;
    public float countTime;
    public Animator animator;
    public float speed;
    public float degree;

    private GameObject target;

    public Transform Ground;
    public bool isGround;
    public float GroundDistance;
    [SerializeField] private LayerMask layerMask;
    public CharacterController controller;
    public float _moveSpeed;



    private float gravity = -9.81f;
    private Vector3 velocityGravity;


    public bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();
        enemyBehavior();
    }

    public void enemyBehavior() {

        if (Vector3.Distance(transform.position, target.transform.position) > 120)
        {
            animator.SetBool("run", false);
            countTime += 1 * Time.deltaTime;
            if (countTime >= 4)
            {
                rutina = Random.Range(0, 2);
                countTime = 0;
            }
            switch (rutina)
            {
                case 0:
                    animator.SetBool("walk", false);
                    break;            
                case 1:               

                    Vector3 direction = Vector3.zero;                

                    direction.x = direction.x + Random.Range(0, 10);                
                    direction.z = direction.z + Random.Range(0, 10);
                    direction.y = velocityGravity.y;
                    direction = direction.normalized;
                                                               
                    controller.Move(direction * 1 * Time.deltaTime);                           
        
                    if (direction != Vector3.zero){
                        
                        float  angle = Mathf.Atan2(direction.x * 1 * Time.deltaTime, direction.z * 1 * Time.deltaTime) * Mathf.Rad2Deg;
                        Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
                        // Rota el personaje hacia la dirección de movimiento
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 10f );
                    
                    }
                    animator.SetBool("walk", true);
                 break;
           }
        }
        else {

            
            if (Vector3.Distance(transform.position, target.transform.position) > 2)
            {
               endAttack();
               var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 4);

                animator.SetBool("walk", false);
                animator.SetBool("run", true);
                animator.SetBool("attack", false);

                // Calcula la dirección en la que el personaje está mirando
                Vector3 direction = transform.rotation * Vector3.forward;
                direction.y = velocityGravity.y;
                direction = direction.normalized;

                float velocity = (_moveSpeed * 0.9f);

                // Mueve el personaje en esa dirección
                controller.Move(direction * velocity * Time.deltaTime);
            }
            else {
                animator.SetBool("walk", false);
                animator.SetBool("run", false);
                animator.SetBool("attack", true);
                isAttacking = true;
            }            
        }
    }

    public void endAttack() {
        animator.SetBool("attack", false);
        isAttacking = false;
    }

    public void Gravity()
    {
        if (isGrounded() && velocityGravity.y < 0)
        {
            velocityGravity.y = -1f;
        }
        else
        {
            velocityGravity.y += gravity;
        }
    }

    public bool isGrounded()
    {
        isGround = Physics.CheckSphere(Ground.position, GroundDistance, layerMask);

        if (isGround) return true;
        return false;
    }
}
