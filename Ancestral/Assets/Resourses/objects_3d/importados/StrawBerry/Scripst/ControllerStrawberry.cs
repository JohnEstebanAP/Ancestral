using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerStrawberry : MonoBehaviour
{

    public GameObject strawberry;

    // Este método se llama cuando otro collider entra en el trigger
    void OnTriggerEnter(Collider other)
    {

        strawberry.SetActive(false);
    }

    // Este método se llama cuando otro collider sale del trigger
    void OnTriggerExit(Collider other)
    {
        // Aquí puedes poner el código que quieras ejecutar
        Debug.Log("Objeto salió del trigger");
    }
}
