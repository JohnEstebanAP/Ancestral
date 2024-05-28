using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalerImage : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    


    private float alturaPantalla = 0;
    private float anchoPantalla =  0;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();        
    }

    private void Update()
    {
    
        AjustarTamanoImagen();
    }

    private void AjustarTamanoImagen()
    {
        Resolution resolucionActual = Screen.currentResolution;
       
        float newAlturaPantalla = resolucionActual.height;       
        float newAnchoPantalla =  resolucionActual.width;

        if (newAlturaPantalla != alturaPantalla)
        {
            print("newAlturaPantalla" + newAlturaPantalla + "alturaPantalla" + alturaPantalla);
            alturaPantalla = newAlturaPantalla;
            anchoPantalla = newAnchoPantalla;

            float anchoSprite = anchoPantalla / 100;
            float altoSprite = alturaPantalla / 100;

            if (anchoPantalla > alturaPantalla)
            {
                anchoSprite = anchoSprite + spriteRenderer.sprite.bounds.size.x;
                altoSprite = altoSprite + spriteRenderer.sprite.bounds.size.y;
            }

            Vector2 nuevoTamano = new Vector2(anchoSprite, altoSprite);
            spriteRenderer.size = nuevoTamano;

        }
    }
}