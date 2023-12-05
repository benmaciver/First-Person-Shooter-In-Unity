using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColour : MonoBehaviour
{
    public Color color;
    // Start is called before the first frame update
    void Start()
    {
        AddComponent<Material>();
        GetComponent<Material>().color = color;
    }

    private void AddComponent<T>()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
