using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<GameObject> Targets;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject target in Targets){
            if (target == null)
                Targets.Remove(target);
            
        }
        if (Targets.Count == 0)
        {
            
            Destroy(gameObject);
        }
    }
}
