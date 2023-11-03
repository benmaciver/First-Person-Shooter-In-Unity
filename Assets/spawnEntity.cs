using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnEntity : MonoBehaviour
{
    public GameObject entity;
    public bool OnRepeat;
    public float repeatTime;
    public Vector3 position;
    public Quaternion rotation;
    private float spawnCooldown;

    // Start is called before the first frame update
    void Start()
    {
        if (position == null)
            position = transform.position;
        if (rotation == null)
            rotation = transform.rotation;
        if (OnRepeat)
            spawnCooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!OnRepeat)
            Instantiate(entity, position, rotation);
        if (OnRepeat)
        {
            if (spawnCooldown <= 0)
            {
                Instantiate(entity, position, rotation);
                spawnCooldown = repeatTime;
            }
            spawnCooldown-=Time.deltaTime;
            
        }


    }
}
