using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<GameObject> Targets;
    public GameObject doorOpenNoise;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> targetsToRemove = new List<GameObject>();

        foreach (GameObject target in Targets)
        {
            if (target == null)
            {
                targetsToRemove.Add(target);
            }
        }

        foreach (GameObject targetToRemove in targetsToRemove)
        {
            Targets.Remove(targetToRemove);
        }

        if (Targets.Count == 0)
        {
            OpenDoor();
        }
    }

    void OpenDoor(){
        Instantiate(doorOpenNoise);
        Destroy(gameObject);
    }
    
}
