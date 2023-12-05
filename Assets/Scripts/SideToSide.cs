using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideToSide : MonoBehaviour
{
    private int multiplier = 1;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0.25f, 1.13f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.(0.25f*multiplier,0,0) * Time.deltaTime;
        transform.Translate(0.25f * multiplier * Time.deltaTime, 0, 0);
        if (transform.position.x > 2.75f)
        {
            multiplier = -1;
        }
        else if (transform.position.x < 0.25f)
        {
            multiplier = 1;
        }
    }
}
