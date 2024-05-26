using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI movesText;
    private int moveCounter;
    private int score;

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

        moveCounter = 30;
        movesText.text = "Movimientos: " + moveCounter;

        score = 0;
        scoreText.text = "Puntos: " + score; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerable GameOver()
    {

        yield return new WaitUntil(() => !BoardManager.sharedInstance.isShifting);
        //creamos un retrasopara que el usuaria pueda ver los cambios por unso segundos
        yield return new WaitForSeconds(0.25f);

        //TODO: invocar a una pantalla diseniada para el Game Over
    }
}
