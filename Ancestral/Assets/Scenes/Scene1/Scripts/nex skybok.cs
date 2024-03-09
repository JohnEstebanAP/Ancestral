using System.Collections;
using UnityEngine;

public class nexskybok : MonoBehaviour
{

    public Material newSkybox;
    public Material oldSkybox;
    public WindZone windZone;
    public float duration = 480.0f; // Duración de la transición en segundos
    Color color1 = new Color(0.6f, 0.82f, 0.89f, 1f); // Color 98D3E5 en formato RGB
    Color color2 = new Color(0.38f, 0.44f, 0.48f, 1f); // Color 626F7B en formato RGB

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entró al Trigger es el jugador
        if (other.gameObject.CompareTag("Player"))
        {
            // Inicia la Corrutina para cambiar gradualmente el Skybox y la densidad de la niebla
            StartCoroutine(ChangeSkyboxAndFog());
        }
    }

    IEnumerator ChangeSkyboxAndFog()
    {
        float t = 0;
        Material tempSkybox = new Material(oldSkybox);

        while (t < duration)
        {
            t += Time.deltaTime;
            float blend = Mathf.Clamp01(t / duration);
            float blend2 = Mathf.Clamp01(t / (duration*2));
            // Cambia gradualmente el Skybox
            tempSkybox.Lerp(oldSkybox, newSkybox, blend2);
            RenderSettings.skybox = tempSkybox;

            // Cambia gradualmente la densidad de la niebla
  
            RenderSettings.fogColor = Color.Lerp(color1, color2, blend);
            RenderSettings.fogDensity = Mathf.Lerp(0.002f, 0.02f, blend);

            // Cambia gradualmente la intensidad del viento
            windZone.windMain = Mathf.Lerp(0f, 2f, blend);

            yield return null;
        }
        RenderSettings.skybox = newSkybox;
    }

}
