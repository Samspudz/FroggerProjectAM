using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject frogPlayer;
    public Transform startPoint;
    public void NewFrog()
    {
        Instantiate(frogPlayer, startPoint.position, startPoint.rotation);
    }
}
