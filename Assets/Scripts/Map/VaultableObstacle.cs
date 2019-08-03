using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultableObstacle : MonoBehaviour {

    [SerializeField]
    private Vector3 sideOne, sideTwo;

    [SerializeField]
    private bool drawGizmos;

    public void Vault(GameObject obj, float vaultTime = 1) {
        var sides = GetSides(obj);
        StartCoroutine(VaultTo(sides.furthest, obj, vaultTime));
    }

    private(Vector3 closest, Vector3 furthest) GetSides(GameObject obj) {
        var distanceToSideOne = ((transform.position + sideOne) - obj.transform.position).magnitude;
        var distanceToSideTwo = ((transform.position + sideTwo) - obj.transform.position).magnitude;

        if (distanceToSideOne < distanceToSideTwo)
            return (closest: transform.position + sideOne, furthest: transform.position + sideTwo);

        return (closest: transform.position + sideTwo, furthest: transform.position + sideOne);

    }

    private IEnumerator VaultTo(Vector3 side, GameObject obj, float vaultTime = 1) {
        var startpos = obj.transform.position;
        var endpos = side;
        var totalVaultTime = vaultTime;
        while (vaultTime > 0) {
            obj.transform.position = Vector3.Lerp(endpos, startpos, 1 / totalVaultTime * vaultTime);
            vaultTime -= Time.deltaTime;
            yield return null;
        }
    }

    private void OnDrawGizmos() {
        if (!drawGizmos)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + sideOne, 0.2f);
        Gizmos.DrawSphere(transform.position + sideTwo, 0.2f);
    }

}