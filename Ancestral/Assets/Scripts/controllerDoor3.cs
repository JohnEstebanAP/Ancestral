using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerDoor3 : MonoBehaviour
{
    public Animator animator;
    private bool isIntoDoor1 = true;
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entró al Trigger es el jugador
        if (other.gameObject.CompareTag("Player"))
        {
            if (isIntoDoor1)
            {
                //StartCoroutine(setStateIsIntoDoor3());
                setStateIsIntoDoor3();
            }

        }

    }

    void setStateIsIntoDoor3()
    {
       // print("******************* parando todas las animaciones **************");
        animator.SetBool("isIntoDoor3", isIntoDoor1);
        // Espera hasta que la animación termine
       // while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
       // {
       //     print("******************* animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1 **************" + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            //print("******************* " + animator.GetCurrentAnimatorStateInfo(0) + "**************");
//            yield return null;
        }
  //      if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.98f)
    //    {
      //      print("finalizando animacion");
       //     animator.enabled = false;
       // }
   // }
}