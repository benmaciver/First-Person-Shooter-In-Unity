using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAt : MonoBehaviour
{
    public GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToTarget = Target.transform.position - transform.position;
        Quaternion lookAwayRotation = Quaternion.LookRotation(-directionToTarget);
        transform.rotation = lookAwayRotation;

    }
}
