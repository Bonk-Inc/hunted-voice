using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerVault : MonoBehaviour {

    private GameObject vaultableObject;

    public bool CanVault { get; private set; }

    private const string VaultableTag = "Vault";

    [SerializeField]
    private UnityEvent VaultStarted, VaultStopped;

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag(VaultableTag))
            return;

        vaultableObject = other.gameObject;
        CanVault = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag(VaultableTag))
            return;

        vaultableObject = null;
        CanVault = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Vault();
        }
    }

    public void Vault() {
        if (!CanVault)
            return;

        var obstacle = vaultableObject.GetComponent<VaultableObstacle>();

        if (obstacle == null) {
            Debug.LogError("No VaultableObstacle found on " + vaultableObject.name);
            return;
        }

        obstacle.Vault(transform.parent.gameObject, 0.2f);
    }

}