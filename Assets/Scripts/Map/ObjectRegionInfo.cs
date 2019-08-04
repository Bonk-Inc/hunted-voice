using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRegionInfo : MonoBehaviour {
    private Region region;

    public Region Region => region;

    public event Action<ObjectRegionInfo> OnRegionChanged;

    public void SetRegion(Region newRegion) {
        if (region == newRegion)
            return;

        region = newRegion;
        OnRegionChanged?.Invoke(this);
    }

}