using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drift : MonoBehaviour
{
    Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = randomDirection();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * Time.deltaTime;
    }
    Vector3 randomDirection(){
        float x = Random.Range(-1f,1f);
        float y = Random.Range(-1f,1f);
        float z = Random.Range(-1f,1f);
        return new Vector3(x,y,z);
    }
}
