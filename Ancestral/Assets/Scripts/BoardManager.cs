using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    public static BoardManager sharedInstance;

    public List<Sprite> prefabs = new List<Sprite>();
    public GameObject currentCandy;

    int xSize = 7;
    int ySize = 9;
    private float alturaPantalla = 0;
    private float anchoPantalla = 0;

    private GameObject[,] candies;

    //Esta cambiando
    public bool isShifting { get; set; }

    private Candy selectedCandy;

    public const int MinCandiesToMatch = 2;

    private float anchoBoardManager = 0;
    private SpriteRenderer spriteRenderer;

    private float padding = 1f;

    public SpriteRenderer backgound;

    public TextMeshProUGUI tMproScore;
    public TextMeshProUGUI tMproMoves;
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
    }

    private void Update()
    {
        float canvasWidth = Screen.width;
        float canvasHeight = Screen.height;

        if (canvasHeight != alturaPantalla)
        {          
            alturaPantalla = canvasHeight;
            anchoPantalla = canvasWidth;
                        
            clearData();

            if (anchoPantalla > alturaPantalla)
            {
                tMproScore.fontSize = 40;
                tMproMoves.fontSize = 40;
                xSize = 12;
                ySize = 7;
            }
            else {
                tMproScore.fontSize = 80;
                tMproMoves.fontSize = 80;
                xSize = 7;
                ySize = 9;            
            }
    
            // Muestra los resultados en la consola
                       
            Vector2 margin = currentCandy.GetComponent<BoxCollider2D>().size * padding;

            float anchoBoarmanager = getSizeBoard(currentCandy, margin[0]);

            if (anchoBoarmanager > anchoPantalla)
            {
                for (int x = 0; x < xSize; x++)
                {
                    for (int y = 0; y < ySize; y++)
                    {
                        padding = 1;
                        gameObject.transform.position = new Vector3(-2.9f, -8, 10);
                        margin = currentCandy.GetComponent<BoxCollider2D>().size * padding;
                    }
                }
            }
            else {
                if ((anchoBoarmanager + 400) < anchoPantalla)
                {
                    gameObject.transform.position = new Vector3(-11.1f, -8.5f, 10);
                }
                else {
                    gameObject.transform.position = new Vector3(-2.9f, -8.5f, 10);
                }
            }
            

            CreateInitialBoard(margin);
        }
    }


    private float getSizeBoard(GameObject currentCandy,float padding) {
        spriteRenderer = currentCandy.GetComponent<SpriteRenderer>();
        Vector3 spriteSize = spriteRenderer.sprite.bounds.size;
        float pixelsPerUnit = spriteRenderer.sprite.pixelsPerUnit;

        float spriteWidthInPixels = spriteSize.x * backgound.sprite.pixelsPerUnit;
        float spriteHeightInPixels = spriteSize.y * backgound.sprite.pixelsPerUnit;

        anchoBoardManager = spriteWidthInPixels * xSize  * padding;

        print("backgoun pixeles" + backgound.sprite.pixelsPerUnit * backgound.sprite.bounds.size.x);
        
        print("pixelsPerUnit" + pixelsPerUnit);
        print("spriteSize.x " + spriteSize.x);
        print("spriteWidthInPixels.x " + spriteWidthInPixels);
        print("ancho boar: " + anchoBoardManager);
        print("ancho pantalla: " + anchoPantalla);

        return anchoBoardManager;
    }

    private void clearData()
    {
        if(candies != null && candies.Length != 0){
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    Destroy(candies[x, y]);
                }
            }
        }        
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

                newCandy.transform.localScale = new Vector3(1f, 1f, 1f);

                newCandy.name = string.Format("Candy[{0}][{1}]", x, y);

                //asignamos un id de la imagen de uno de todos los caramelos
                //tambien se verifica que el caramelo elegido no se repita con uno de sus vecinos
                do
                {
                    idx = Random.Range(0, prefabs.Count);
                } while ((x > 0 && idx == candies[x - 1, y].GetComponent<Candy>().id) ||
                  (y > 0 && idx == candies[x, y - 1].GetComponent<Candy>().id));



                Sprite sprite = prefabs[idx];
                newCandy.GetComponent<SpriteRenderer>().sprite = sprite;
                newCandy.GetComponent<Candy>().id = idx;

                //le asignamos los caramelos como hijos del board manager
                newCandy.transform.parent = this.transform;

                //guardamos el caramelo creado en una matrix.
                candies[x, y] = newCandy;

            }
        }


        if (anchoBoardManager > anchoPantalla)
        {
            print("ingreso al condicional 1");
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {                    
                    candies[x, y].transform.localScale = new Vector3(1f, 1f, 1f);
                    candies[x, y].transform.position = calculateNewPosicionCandy(startX, startY, margin, x, y);
                }
            }        
        }
        else {
            if ((anchoBoardManager + 400) < anchoPantalla) {

                padding = 2;

                Vector2 newMargin = currentCandy.GetComponent<BoxCollider2D>().size * padding;

                print("ingreso al condicional 2");
                for (int x = 0; x < xSize; x++)
                {
                    for (int y = 0; y < ySize; y++)
                    {
                        candies[x, y].transform.localScale = new Vector3(2f, 2f, 2f);
                        candies[x, y].transform.position = calculateNewPosicionCandy(startX, startY, newMargin, x, y);
                    }
                }
            }            
        }
        print("**************************");
        getSizeBoard(candies[0, 0], margin[0]);
        print("**************************");
    }

    
    private Vector3 calculateNewPosicionCandy(float startX, float startY, Vector2 margin, int x, int y)
    {

        Vector3 posicion = new Vector3(startX + (margin.x * x), startY + (margin.y * y), 0);

        return posicion;
    }
    
    
    public IEnumerator FindNullCandies()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                if (candies[x, y].GetComponent<SpriteRenderer>().sprite == null)
                {
                    yield return StartCoroutine(MakeCandiesFall(x, y));
                    break;
                }
            }
        }


        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                candies[x, y].GetComponent<Candy>().FindAllMatches();
            }
        }

    }
    
    private IEnumerator MakeCandiesFall(int x, int yStart, float shiftDelay = 0.05f)
    {
        isShifting = true;

        List<SpriteRenderer> renderes = new List<SpriteRenderer>();
        int nullCandies = 0;

        for (int y = yStart; y < ySize; y++)
        {
            SpriteRenderer spriteRenderer = candies[x, y].GetComponent<SpriteRenderer>();
            if (spriteRenderer.sprite == null)
            {
                nullCandies++;
            }
            renderes.Add(spriteRenderer);
        }

        for (int i = 0; i < nullCandies; i++)
        {
            //ingrementamos el puntaje de los parejas encontradas
            GUIManager.sharedInstance.Score += 10;

            yield return new WaitForSeconds(shiftDelay);

            if (renderes.Count == 1) {
                for (int j = 0; j < renderes.Count; j++)
                {                   
                    renderes[j].sprite = GetNewCandy(x, ySize - 1);
                }
            }
            else{
                for (int j = 0; j < renderes.Count - 1; j++)
                {
                    renderes[j].sprite = renderes[j + 1].sprite;
                    renderes[j + 1].sprite = GetNewCandy(x, ySize - 1);
                }
            }
            
        }

        isShifting = false;
    }
    
    
    private Sprite GetNewCandy(int x, int y)
    {
        List<Sprite> possibleCandies = new List<Sprite>();
        possibleCandies.AddRange(prefabs);

        if (x > 0)
        {
            possibleCandies.Remove(
                candies[x - 1, y].GetComponent<SpriteRenderer>().sprite);
        }
        if (x < xSize - 1)
        {
            possibleCandies.Remove(
                candies[x + 1, y].GetComponent<SpriteRenderer>().sprite);
        }
        if (y > 0)
        {
            possibleCandies.Remove(
                candies[x, y - 1].GetComponent<SpriteRenderer>().sprite);
        }

        return possibleCandies[Random.Range(0, possibleCandies.Count)];
    }
}
