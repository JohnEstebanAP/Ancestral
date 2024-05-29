using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI movesText;
    public TextMeshProUGUI timerText;
    private int moveCounter;
    private int score;
    private float timer = 120;

    public int Score
    {
        get {
            return score;
        }

        set
        {
            score = value;
            scoreText.text = "Puntos: " + score;
        }
    }

    public int MoveCounter
    {
        get
        {
            return moveCounter;
        }

        set
        {
            moveCounter = value;
            movesText.text = "Movimientos: " + moveCounter;

            if (moveCounter <= 0)
            {
                moveCounter = 0;
                //StartCoroutine(GameOver());
            }
        }
    }


    public float Timer
    {
        get
        {
            return timer;
        }

        set
        {
            timer = value;                               
            
        }
    }

    //hacemos que nustra clase GUIMAnager tenga un patron singleton
    public static GUIManager sharedInstance;



    // Start is called before the first frame update
    void Start()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        moveCounter = 32;
        movesText.text = "Movimientos: " + moveCounter;

        score = 0;
        scoreText.text = "Puntos: " + score; 
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text= "Tiempo:"+ timer.ToString("f0");
        print("score"+ score);
        if (timer <= 0 && score < 1000)
        {
            print("fin del juego");
            SceneManager.LoadScene("GameOver");
        }
        
        if (timer <= 0 && score >= 1000) {
           SceneManager.LoadScene("premio");   
        }
        
    }

    private IEnumerable GameOver()
    {

        yield return new WaitUntil(() => !BoardManager.sharedInstance.isShifting);
        //creamos un retrasopara que el usuaria pueda ver los cambios por unso segundos
        yield return new WaitForSeconds(0.25f);

        //TODO: invocar a una pantalla diseniada para el Game Over
    }
}
