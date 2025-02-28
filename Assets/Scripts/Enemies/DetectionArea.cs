using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    public event Action<Collider> onDetectionEnter;
    public event Action<Collider> onDetectionExit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        onDetectionEnter?.Invoke(other);
    }

    void OnTriggerExit(Collider other)
    {
        onDetectionExit?.Invoke(other);
    }
}
