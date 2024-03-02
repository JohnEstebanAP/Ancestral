using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nexsenecave : MonoBehaviour
{
    // Asegúrate de que el nombre de la escena sea el correcto
    public string nombreDeLaEscena;

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entró al Trigger es el jugador
        if (other.gameObject.CompareTag("Jugador"))
        {
            // Cambia a la nueva escena
            SceneManager.LoadScene(nombreDeLaEscena);
        }
    }
}
