using System.Collections;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light directionalLight;
    public Light[] spotLights;      // Tableau contenant tous les Spot Lights
    private bool isInside = true;   // Le joueur commence dans le collider
    private Coroutine flickerCoroutine; // Gère le clignotement

    private void Start()
    {
        SetLighting(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isInside)
        {
            SetLighting(true); // Le joueur entre -> Directional ON, Spots OFF
            isInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isInside)
        {
            SetLighting(false); // Le joueur sort -> Directional OFF, Spots ON
            isInside = false;
        }
    }

    private void SetLighting(bool inside)
    {
        if (directionalLight != null)
        {
            directionalLight.enabled = inside;
        }

        for (int i = 0; i < spotLights.Length; i++)
        {
            if (spotLights[i] != null)
            {
                spotLights[i].enabled = !inside;
            }
        }

        // Démarrer/Arrêter le clignotement
        if (!inside && spotLights.Length > 0 && spotLights[0] != null)
        {
            flickerCoroutine = StartCoroutine(FlickerLight(spotLights[0]));
        }
        else if (inside && flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine);
            spotLights[0].enabled = false; // Éteint la lumière quand le joueur rentre
        }
    }

    private IEnumerator FlickerLight(Light light)
    {
        while (true)
        {
            light.enabled = !light.enabled; // Allumer/éteindre
            yield return new WaitForSeconds(Random.Range(0.3f, 1f)); // Temps aléatoire entre 0.3s et 1s
        }
    }
}