using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextSceneMiniGameThreeInRowPuzzle : MonoBehaviour
{
    // Asegúrate de que el nombre de la escena sea el correcto
    private  string nameScene;


    // Start is called before the first frame update
    void Start()
    {
        nameScene = "MiniGameThreeInRowPuzzle";
    }

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entró al Trigger es el jugador
        if (other.gameObject.CompareTag("Player"))
        {
            // Cambia a la nueva escena
            SceneManager.LoadScene(nameScene);
        }
    }
}
