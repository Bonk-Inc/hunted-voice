using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private Transform obj;

    [SerializeField]
    private float speed;

    private void LateUpdate() {
        var target = obj.transform.position;
        target.y = transform.position.y;
        var direction = target - transform.position;
        var distance = direction.magnitude;

        var smoothPos = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
        transform.position = smoothPos;
    }

    public void ChangeTarget(GameObject newTarget) {
        obj = newTarget.transform;
    }

}