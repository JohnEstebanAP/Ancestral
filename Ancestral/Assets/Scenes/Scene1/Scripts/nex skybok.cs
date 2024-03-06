using System.Collections;
using UnityEngine;

public class nexskybok : MonoBehaviour
{

    public Material newSkybox;
    public Material oldSkybox;
    public WindZone windZone;
    public float duration = 10.0f; // Duración de la transición en segundos

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
            RenderSettings.fogDensity = Mathf.Lerp(0.001f, 0.01f, blend);

            // Cambia gradualmente la intensidad del viento
            windZone.windMain = Mathf.Lerp(0f, 3.5f, blend);

            yield return null;
        }
        RenderSettings.skybox = newSkybox;
    }

}
