using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{

    public TextMeshProUGUI dialogueText;
    private List<string> linesIntro = new List<string>();
    private List<string> linesHistoriMadremonte1 = new List<string>();
    public List<AudioSource> listAudios = new List<AudioSource>();
    private List<List<string>> capitulo = new List<List<string>>();
    List<string> capituloSelecte;
    
    public float textSpeed = 0.1f;
    public int indexCapitulo = 0;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = string.Empty;

        linesIntro.Add("¡Hola! Soy Esmeralda, una investigadora que ha pasado 2 años estudiando la cultura antioqueña. Ahora estoy emocionada de volver a mi hogar después de tanto tiempo, mi familia vive en el municipio de Andes, en el suroeste de Antioquia");
        linesIntro.Add("Andes es un lugar mágico en la cordillera de los Andes Occidental.  Limita al norte con Hispania, Pueblo Rico y Betania; al sur con Risaralda; al oeste con Chocó; y al este con Jericó y Jardín. Con paisajes montañosos, cafetales, calles empedradas y casas coloniales, este municipio es un destino lleno de belleza natural y rica cultura. Rodeado de montañas y ríos cristalinos, es perfecto para aventuras al aire libre. Conocido por sus festividades tradicionales y su vibrante comunidad, Andes es un lugar donde la hospitalidad de su gente te hará sentir como en casa.");
        linesIntro.Add("Estoy muy emocionada, he traído muchos regalos para mi familia, incluyendo ropa y objetos que encontré durante mis investigaciones.");
        linesIntro.Add("El camino a casa es una trocha, una carretera sin pavimentar con barro y rocas. Pero si sigo derecho, según el mapa, llegaré a mi hogar. ¡Acompáñame en esta aventura!");


        linesHistoriMadremonte1.Add("Te estaba esperando, chiquilla. Estoy muy enojada contigo y con tu especie porque han invadido, destruido el bosque y la naturaleza. Sin embargo, he visto en el interior de tu corazón que hay bondad. Por eso, te perdonaré y te propondré un trato. ");
        linesHistoriMadremonte1.Add("Te ayudaré a recuperar tus artefactos perdidos y a regresar a casa, pero antes, deberás ayudarme a vencer a la bruja de las banderas negras. Ella ha hechizado a los criminales de la región y está destruyendo mi preciado bosque.");
        linesHistoriMadremonte1.Add("Para empezar, te regalaré un sombrero aguadeño para tu colección de artefactos vestimentarios. Ahora, regresa y entra en el lago dentro de la cueva. Allí encontrarás un portal mágico escondido que te llevará fuera de esta cueva, pero para poder activarlo deberás superar un reto. Te deseo suerte, pequeña Esmeralda.");        

        capitulo.Add(linesIntro);
        capitulo.Add(linesHistoriMadremonte1);

        capituloSelecte = capitulo[indexCapitulo];

        StartDialogue();
 }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void OnClick()
    {   
 

            if (dialogueText.text ==  capituloSelecte[index])
            {
                NextLine();
            }
            else
            {
               StopAllCoroutines();      
               dialogueText.text = linesIntro[index];                
            }        
    }


    public void StartDialogue() { 
        index = 0;


        StartCoroutine(WriteLine());

    }

    IEnumerator WriteLine()
    {

        listAudios[index].Play();
        print("Index:"+ index);
        foreach (var letter in capituloSelecte[index].ToCharArray()) {
            dialogueText.text += letter;

            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void NextLine() {
        if (index < capituloSelecte.Count -1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(WriteLine());
        }
        else {
            gameObject.SetActive(false);
        }               
    }
}

