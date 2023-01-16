using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{

    public Text scoreText;
    public Text movesText;
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
            scoreText.text = "Socore: " + score;
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
            movesText.text = "Moves: " + moveCounter;
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
        movesText.text = "Moves: " + moveCounter;

        score = 0;
        scoreText.text = "Socore: " + score; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
