using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class ControllerHorse: MonoBehaviour
{
    public Animator animator;

    private GameObject target;
    public CharacterController controller;
    public float _moveSpeed;

    private float gravity = -9.81f;
    private Vector3 velocityGravity;
    private bool wooded = false;

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

        if (Vector3.Distance(transform.position, target.transform.position) > 5f && !wooded)
        {
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 4);

            animator.SetBool("walk", true);

            // Calcula la dirección en la que el personaje está mirando
            Vector3 direction = transform.rotation * Vector3.forward;
            direction.y = velocityGravity.y;
            direction = direction.normalized;

           
            // Mueve el personaje en esa dirección
            controller.Move(direction * _moveSpeed * Time.deltaTime);
        }else if(wooded){
            controller.Move(Vector3.forward * _moveSpeed * Time.deltaTime);
        }
        else {
            animator.SetBool("walk", false);
        }                   
    }


    public void Gravity()
    {
        if (velocityGravity.y < 0)
        {
            velocityGravity.y = -1f;
        }
        else
        {
            velocityGravity.y += gravity;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("wooded"))
        {
            wooded = true;
            print("iniciar uida del caballo");
        }
    }
}
