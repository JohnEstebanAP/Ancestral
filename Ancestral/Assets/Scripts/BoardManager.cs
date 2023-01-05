using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager sharedInstance;

    public List<Sprite> prefabs = new List<Sprite>();
    public GameObject currentCandy;
    public int xSize, ySize;

    private GameObject[,] candies;
    public bool isShifting { get; set; }

    private Candy selectedCandy;

    public const int MinCandiesToMatch = 2;

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


        Vector2 margin = currentCandy.GetComponent<BoxCollider2D>().size;
        CreateInitialBoard(margin);
    }


    private void CreateInitialBoard(Vector2 margin)
    {
        candies = new GameObject[xSize, ySize];

        float startX = this.transform.position.x;
        float startY = this.transform.position.y;

        int idx = -1;


        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                GameObject newCandy = Instantiate(
                    currentCandy,
                    calculateNewPosicionCandy(startX, startY, margin, x, y),
                    currentCandy.transform.rotation
                    );

                newCandy.name = string.Format("Candy[{0}][{1}]", x, y);

                //se guarda cada caramelo creado en un array
                candies[x, y]= newCandy;

                do
                {
                    idx = Random.Range(0, prefabs.Count);
                } while ((x > 0 && idx == candies[x - 1, y].GetComponent<Candy>().id) ||
                  (y > 0 && idx == candies[x, y - 1].GetComponent<Candy>().id));



                Sprite sprite = prefabs[idx];
                newCandy.GetComponent<SpriteRenderer>().sprite = sprite;
                newCandy.GetComponent<Candy>().id = idx;

                newCandy.transform.parent = this.transform;
                candies[x, y] = newCandy;

            }
        }

    }

    private Vector3 calculateNewPosicionCandy(float startX, float startY, Vector2 margin, int x, int y)
    {

        Vector3 posicion = new Vector3(startX + (margin.x * x), startY + (margin.y * y), 0);

        return posicion;
    }



    // Update is called once per frame
    void Update()
    {

    }
}
