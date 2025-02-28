using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    public Transform a;
    public Transform b;
    private float elapsedTime;
    private DetectionArea plateforme;

    // Start is called before the first frame update
    void Start()
    {
        plateforme = GetComponentInChildren<DetectionArea>();
        plateforme.onDetectionEnter += DetectionEnter;
        plateforme.onDetectionExit += DetectionExit;
    }

    // Update is called once per frame
    void Update()
    {
        plateforme.transform.position = Vector3.Lerp(a.position, b.position, Mathf.Sin(elapsedTime)/2 + 0.5f);
        elapsedTime += Time.deltaTime;
    }

    private void DetectionEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.SetParent(plateforme.transform);
            Debug.Log("Le joueur entre en collision");
        }
    }

    private void DetectionExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.SetParent(null);
            Debug.Log("Le joueur n'est plus en collision");
        }
    }
}
