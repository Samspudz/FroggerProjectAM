using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPadScript : MonoBehaviour
{
    public SpriteRenderer _spr;
    public bool _occupied;

    void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
        _spr.enabled = false;
        _occupied = false;
    }


}
