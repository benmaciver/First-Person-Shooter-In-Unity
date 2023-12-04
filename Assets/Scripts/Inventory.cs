using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject[] inventory;
    private int currentIndex = 0;
    private float animationExpTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        inventory = getChildren();
        updateInventory();
    }

    // Update is called once per frame
    void Update()
    {
        // Scroll through inventory on mouse wheel input
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0f)
        {
            animationExpTime = Time.time + inventory[currentIndex].GetComponent<Weapon>().Stash();
            ChangeCurrentIndex((int)Mathf.Sign(scrollWheel));
            
        }
        if (animationExpTime < Time.time && animationExpTime != -1)
        {
            inventory[currentIndex].SetActive(true);
            inventory[GetPrevIndex()].SetActive(false);
            if (inventory[currentIndex]?.GetComponent<Weapon>() != null)
            {
                animationExpTime = Time.time + inventory[currentIndex].GetComponent<Weapon>().Equip();
            }
            animationExpTime =-1;

        }
    }

    private GameObject[] getChildren()
    {
        GameObject[] arr = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            arr[i] = transform.GetChild(i).gameObject;
        }
        return arr;
    }

    private void ChangeCurrentIndex(int direction)
    {
        currentIndex += direction;

        // Ensure index stays within bounds
        if (currentIndex < 0)
            currentIndex = inventory.Length - 1;
        else if (currentIndex >= inventory.Length)
            currentIndex = 0;
    }
    private int GetPrevIndex()
    {
        int prevIndex = currentIndex - 1;
        if (prevIndex < 0)
            prevIndex = inventory.Length - 1;
        return prevIndex;
    }
    private void updateInventory()
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            inventory[i].SetActive(false);
        }
        inventory[currentIndex].SetActive(true);
    }
}
