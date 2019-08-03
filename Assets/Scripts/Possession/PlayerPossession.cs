using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPossession : MonoBehaviour {
    
    [SerializeField]
    private float maxDistance = 1;

    private MapController mapController;

    private void start(){
        mapController = MapController.Instance;
    }

    private void OnMouseOver(){
        if(CalculateDistance(this.gameObject, mapController.CurrentPlayer) >= maxDistance) return;

    }

    private float CalculateDistance(GameObject currentObject, GameObject otherObject){
        return 0;
    }
}