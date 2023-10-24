using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleDiveScript : MonoBehaviour
{
    [SerializeField] TurtleAnimationScript[] _turtle;
    public Collider2D killCollider;
    [SerializeField] private bool isDiving;


    void Start()
    {
        killCollider.enabled = false;
        isDiving = false;
        InvokeRepeating("TurtleDive", 3, 10);
    }

    void TurtleDive()
    {
        StartCoroutine(DiveTime());
    }

    IEnumerator DiveTime()
    {
        if (!isDiving)
        {
            foreach (TurtleAnimationScript t in _turtle) 
            {
                t.TurtleDive();
            }
            yield return new WaitForSeconds(0.5f);

            foreach (TurtleAnimationScript t in _turtle)
            {
                t.TurtleSurface();
            }
        }
    }
}
