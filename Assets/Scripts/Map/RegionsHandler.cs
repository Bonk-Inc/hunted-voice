using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionsHandler : MonoBehaviour {

    [SerializeField]
    private Region[] regions;

    private void Awake() {
        foreach (var region in regions) {
            region.ObjectEnteredRegion += RegisterObjectToRegion;
        }
    }

    private void RegisterObjectToRegion(GameObject obj, Region region) {
        var objectInfo = obj.GetComponent<ObjectRegionInfo>();
        if (objectInfo == null)
            return;

        objectInfo.SetRegion(region);
    }

}