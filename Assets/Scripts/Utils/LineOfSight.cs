using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour {

    [Header("Line Of Sight")]
    [SerializeField, Range(0, 360)] private float viewAngle = 90;
    [SerializeField] private float viewRadius = 30;

    [Header("Objects")]
    [SerializeField] private LayerMask target;
    [SerializeField] private LayerMask obstacle = -1;

    [Header("Gizmos")]
    [SerializeField] private bool drawGizmos = false;
    [SerializeField] private bool drawLines = true;
    [SerializeField] private bool drawSphere = false;

    public float ViewRadius { get { return viewRadius; } set { viewRadius = value; } }
    public float ViewAngle { get { return viewAngle; } set { viewAngle = value; } }

    public GameObject[] GetObjectsInView() {
        var area = Physics.OverlapSphere(transform.position, viewRadius);
        List<GameObject> objectsInView = new List<GameObject>();
        for (int i = 0; i < area.Length; i++) {

            if (target != (target | (1 << area[i].gameObject.layer)))
                continue;

            RaycastHit ray;

            if (Physics.Raycast(transform.position, area[i].transform.position - transform.position, out ray, Mathf.Infinity, obstacle + target)) {
                if (target != (target | (1 << ray.collider.gameObject.layer)))
                    continue;

                var dir = area[i].transform.position - transform.position;
                if (Vector3.Angle(dir, transform.forward) < viewAngle / 2) {
                    objectsInView.Add(area[i].gameObject);
                }

            }
        }

        return objectsInView.ToArray();

    }

    public bool IsObjectInView(GameObject obj) {

        if ((obj.transform.position - transform.position).magnitude > viewRadius)
            return false;

        RaycastHit ray;

        if (!Physics.Raycast(transform.position, obj.transform.position - transform.position, out ray, Mathf.Infinity, obstacle + target))
            return false;

        if (target != (target | (1 << ray.collider.gameObject.layer)))
            return false;

        var dir = obj.transform.position - transform.position;
        if (Vector3.Angle(dir, transform.forward) < viewAngle / 2)
            return true;

        return false;

    }

    private void OnDrawGizmos() {
        if (!drawGizmos)
            return;

        if (drawSphere)
            Gizmos.DrawWireSphere(transform.position, viewRadius);

        if (!drawLines)
            return;

        var left = (Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward);
        Gizmos.DrawLine(transform.position + left, transform.position + left + left.normalized * (viewRadius - 1));
        var right = (Quaternion.Euler(0, viewAngle / 2, 0) * transform.forward);
        Gizmos.DrawLine(transform.position + right, transform.position + right + right.normalized * (viewRadius - 1));

    }

    public void SetTargetMask(LayerMask mask) {
        target = mask;
    }

    public void SetNonObstacleMask(LayerMask mask) {
        obstacle = mask;
    }

}