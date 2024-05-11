using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class logiBarLife : MonoBehaviour
{
    public int lifeMax;
    public float lifeNow;
    public Image imagenBarLife;

    // Start is called before the first frame update
    void Start()
    {
        lifeNow = lifeMax;        
    }

    // Update is called once per frame
    void Update()
    {
           ResetLife();
        if (lifeNow <= 0) { 
            gameObject.SetActive(false);
        }
    }

    public void ResetLife()
    {
        imagenBarLife.fillAmount = lifeNow / lifeMax;
    }
}
