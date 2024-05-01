using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerDoor1 : MonoBehaviour
{
    public Animator animator;
    private bool isIntoDoor1 = true;
    private void OnTriggerEnter(Collider other)
    {
        print("ingreso ene trigger");
        // Comprueba si el objeto que entró al Trigger es el jugador
        if (other.gameObject.CompareTag("Player"))
        {
            print("es el player");
            if (isIntoDoor1) {
                StartCoroutine(setStateIsIntoDoor1());
            }
            
        }

    }

    IEnumerator setStateIsIntoDoor1()
    {
        animator.SetBool("isIntoDoor1", isIntoDoor1);
        yield return new WaitForSeconds(1); // Espera 1 segundo
        isIntoDoor1 = false;
        animator.SetBool("isIntoDoor1", isIntoDoor1);
    }
}
