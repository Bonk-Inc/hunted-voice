using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    private static MapController instance;

    public static MapController Instance{get{return instance;}}

    //Todo change when there is a player class
    private GameObject currentPlayer;

    private void Awake()
    {
        if(!instance)
            instance = this;
        else
            Destroy(this);
    }

   
}
