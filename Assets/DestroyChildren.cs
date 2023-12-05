using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChildren : MonoBehaviour
{
    private GameObject[] children;
    // Start is called before the first frame update
    void Start()
    {
        children = getChildren();
    }

    // Update is called once per frame
    void Update()
    {
        if (allChildrenInactive()){
            Destroy(gameObject);
        }
    }
    public GameObject[] getChildren(){
        GameObject[] children = new GameObject[transform.childCount];
        int i = 0;
        foreach (Transform child in transform){
            children[i] = child.gameObject;
            i++;
        }
        return children;
    }
    public bool allChildrenInactive(){
        foreach (Transform child in transform){
            if (child.gameObject.activeSelf){
                return false;
            }
        }
        return true;
}
}
