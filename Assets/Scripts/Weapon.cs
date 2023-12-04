using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public AnimationClip stash;
    public AnimationClip equip;

    protected Animation anim;
    // Start is called before the first frame update
    protected void Start()
    {
        anim = GetComponent<Animation>();
        anim.AddClip(stash, "Stash");
        anim.AddClip(equip, "Equip");
    }

    public float Stash(){
        anim.Play("Stash");
        return stash.length;
    }
    public float Equip(){
        anim.Play("Equip");
        return equip.length;
    }
    
}
