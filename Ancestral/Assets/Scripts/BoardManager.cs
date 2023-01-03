using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager sharedInstance;

    public List<Sprite> prefabs = new List<Sprite>();
    public GameObject currentCandy;
    public int xSize, ySize;

    private GameObject[,] candies;
    public bool isShifting { get; set; }
    
    // Start is called before the first frame update
    void Start()
    {
        
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        Vector2 margin = currentCandy.GetComponent<BoxCollider2D>().size;
        CreateInitialBoard(margin);
    }


    private void CreateInitialBoard(Vector2 margin)
    {

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
