using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Candy : MonoBehaviour
{

    public int id;

    private static Color selectedColor = new Color(0.5f, 0.5f, 0.5f,1.0f);
    private static  Candy previousSelected= null;

    private SpriteRenderer spriteRenderer;
    private bool isSelected = false;

    private Vector2[] adjacentDirections = new Vector2[]
    {
        Vector2.up, Vector2.down, Vector2.left, Vector2.right,
    };

    //este metodo se ejecuta antes del start, parar cargar datos antes
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SelectCandy()
    {
        isSelected= true;
        spriteRenderer.color = selectedColor;
        previousSelected= gameObject.GetComponent<Candy>();
    }

    private void DeselectCandy()
    {
        isSelected= false;
        spriteRenderer.color = Color.white;
        previousSelected = null;
    }

    private void OnMouseDown()
    {
        //Validacion en caso de que se este selecionnado un cuadro basido
        if (spriteRenderer.sprite == null
            || BoardManager.sharedInstance.isShifting )
        {
            return;
        }

        if (isSelected)
        {
            DeselectCandy();
        }else{
            if(previousSelected == null)
            {
                SelectCandy();
            }
            else
            {
                //se valida se se esta realisando un movimiento de caramelos
                if (CanSwipe())
                {
                    SwapSprites(previousSelected);
                    previousSelected.FindAllMatches();
                    previousSelected.DeselectCandy();
                    FindAllMatches();

                    GUIManager.sharedInstance.MoveCounter--;
                }
                else
                {
                    previousSelected.DeselectCandy();
                    SelectCandy();
                }

            }
        }
        
    }

    //intercambio de caramelos
    public void SwapSprites(Candy newCandy)
    {
        //En caso de que los dos caramelos selecionados sean iguales
        if(spriteRenderer.sprite == 
            newCandy.GetComponent<SpriteRenderer>().sprite) return;
        
        Sprite oldCandy = newCandy.spriteRenderer.sprite;
        newCandy.spriteRenderer.sprite = spriteRenderer.sprite;
        spriteRenderer.sprite = oldCandy;


        int tempId = newCandy.id;
        newCandy.id = this.id;
        this.id = tempId;
    }

    //otener los becinos en una sola direcion
    private GameObject GetNeighbor(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction);

        //el rallo choco o coliciono con algun objeto 
        if(hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null; 
        }
    }

    //otiene la lista de todas los becinos del caramelo
    private List<GameObject> GetAllNeighbors()
    {
        List<GameObject> neighbors = new List<GameObject>();

        foreach(Vector2 direction in adjacentDirections) { 
          neighbors.Add(GetNeighbor(direction));
        }

        return neighbors;
    }

    //retorna un bool si se pede intercambiar caramelos
    private bool CanSwipe()
    {
        return GetAllNeighbors().Contains(previousSelected.gameObject);
    }


    //encontrar parejas al rededor de el caramelo
    private List<GameObject> FindMatch(Vector2 direction)
    {
        List<GameObject> matchingCandies = new List<GameObject>();

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position,
                                         direction);
        while (hit.collider != null &&
              hit.collider.GetComponent<SpriteRenderer>().sprite ==
                 spriteRenderer.sprite)
        {
            matchingCandies.Add(hit.collider.gameObject);
            hit = Physics2D.Raycast(hit.collider.transform.position,
                                    direction);
        }

        return matchingCandies;
    }

    //podemos eliminar pareja de caramelos
    private bool clearMatch(Vector2[] directions)
    {
        List<GameObject> matchingCandies = new List<GameObject>();
      
        foreach(Vector2 direction in directions)
        {
            matchingCandies.AddRange(FindMatch(direction));
        }

        if(matchingCandies.Count >= BoardManager.MinCandiesToMatch) { 
            foreach(GameObject candy in matchingCandies)
            {
                //dejamos su componente visul nula para no mostrar el tipo de caramelo
                candy.GetComponent<SpriteRenderer>().sprite = null;
            }
            return true;
        }
        return false;
        
    }


    //encontrar todas la parejas o coinsidencias de los caramelos
    public void FindAllMatches()
    {
        if(spriteRenderer.sprite == null)
        {
            return;
        }

        bool hMatch = clearMatch(new Vector2[2] {Vector2.left, Vector2.right});

        bool vMatch = clearMatch(new Vector2[2] { Vector2.up, Vector2.down });

        if( hMatch || vMatch)
        {
            spriteRenderer.sprite = null;

            StopCoroutine(BoardManager.sharedInstance.FindNullCandies());
            StartCoroutine(BoardManager.sharedInstance.FindNullCandies());
        }
    }
    
}
