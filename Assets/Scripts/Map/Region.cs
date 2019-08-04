using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Region : MonoBehaviour {

    [SerializeField]
    private string customName = string.Empty;

    [SerializeField]
    private List<string> tags;
    [SerializeField]
    private bool blacklistTags = true;
    [SerializeField]
    private Transform[] waypoints;

    private List<GameObject> currentObjects = new List<GameObject>();

    public event Action<GameObject, Region> ObjectEnteredRegion, ObjectLeftRegion;

    public string RegionName {
        get {
            return customName != string.Empty ? customName : name;
        }
    }

    public int ObjectCount => currentObjects.Count;

    public Collider Collider { get; private set; }

    private void Awake() {
        Collider = GetComponent<Collider>();
    }

    public Transform[] GetWaypoints() {
        return waypoints;
    }

    public bool IsInRegion(GameObject obj) {
        return currentObjects.Contains(obj);
    }

    private void OnTriggerEnter(Collider other) {

        if (blacklistTags && tags.Contains(other.tag) || !blacklistTags && !tags.Contains(other.tag))
            return;

        currentObjects.Add(other.gameObject);
        ObjectEnteredRegion?.Invoke(other.gameObject, this);
    }

    private void OnTriggerExit(Collider other) {
        if (!currentObjects.Contains(other.gameObject))
            return;

        currentObjects.Remove(other.gameObject);
        ObjectLeftRegion?.Invoke(other.gameObject, this);

    }

}