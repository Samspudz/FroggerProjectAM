using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleAnimationScript : MonoBehaviour
{
    Animator anim;
    TurtleDiveScript turtleDiveScript;

    void Start()
    {
        anim = GetComponent<Animator>();
        turtleDiveScript = GetComponentInParent<TurtleDiveScript>();
    }

    public void TurtleDive()
    {
        anim.SetTrigger("Dive");
    }

    public void TurtleSurface()
    {
        anim.SetTrigger("Surface");
    }
    
    public void ColliderDisable()
    {
        turtleDiveScript.killCollider.enabled = true;
    }

    public void ColliderEnable()
    {
        turtleDiveScript.killCollider.enabled = false;
    }
}
