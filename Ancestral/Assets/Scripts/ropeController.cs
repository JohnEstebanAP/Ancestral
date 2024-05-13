using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ropeController : MonoBehaviour
{
    private GameObject horse;
    public HingeJoint hingeJoint;
    // Start is called before the first frame update
    void Start()
    {
       horse =  GameObject.FindGameObjectWithTag("horse");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("ingeso ene l triger de la cuerda");
        if (other.CompareTag("wooded"))
        {
            print("solto el laso");
           hingeJoint.connectedBody = horse.GetComponent<Rigidbody>();                
        }
    }
}
