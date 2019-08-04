using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspicionMeter : MonoBehaviour {

    [SerializeField]
    private LineOfSight lineOfSight;

    [SerializeField]
    private float LosCheckDistance;

    [SerializeField]
    private float runWatchSuspicion = 60;

    [SerializeField]
    private float suspicionDistance, distanceSuspicion;

    [SerializeField]
    private float maxSuspicion;

    [SerializeField]
    private bool drawGizmos;

    [SerializeField]
    private CitizenStateMachine stateMachine;

    private const string playerTag = "Player";

    private float suspicion = 0;

    private GameObject currentPlayer;

    private bool isWatching = false;

    private void Start() {
        currentPlayer = PlayerSingleton.Instance.CurrentPlayer;

        PlayerSingleton.Instance.PlayerChanged.AddListener((newPlayer) => {
            currentPlayer = newPlayer;
        });

        PlayerSingleton.Instance.OnPlayerChanged += (oldPlayer, newPlayer) => {
            if (!isWatching || oldPlayer == gameObject || newPlayer == gameObject) {
                suspicion = 0;
                return;
            }

            if (lineOfSight.IsObjectInView(oldPlayer) || lineOfSight.IsObjectInView(newPlayer)) {
                suspicion = maxSuspicion + 1;
                return;
            }

            suspicion = 0;
        };
    }

    public void StartWatching() {
        isWatching = true;
        StartCoroutine(WatchForPlayer());
        StartCoroutine(WatchDistanceToPlayer());

        StartCoroutine(WatchSuspision());
    }

    public void StopWatching() {
        isWatching = false;
        StopAllCoroutines();
        suspicion = 0;
    }

    private IEnumerator WatchSuspision() {
        while (true) {
            yield return new WaitForEndOfFrame();
            if (suspicion > maxSuspicion) {
                stateMachine.SetState(CitizenStateType.TravelingToPoint);
                break;
            }
        }
    }

    private IEnumerator WatchDistanceToPlayer() {
        yield return null;
        while (true) {
            if ((currentPlayer.transform.position - transform.position).magnitude < suspicionDistance)
                suspicion += distanceSuspicion * Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator WatchForPlayer() {
        yield return null;
        while (true) {

            while ((currentPlayer.transform.position - transform.position).magnitude < LosCheckDistance) {
                if (lineOfSight.IsObjectInView(currentPlayer)) {
                    yield return StartCoroutine(WatchRunning(currentPlayer));
                }
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }
    }

    private IEnumerator WatchRunning(GameObject player) {
        MovementController mc = player.GetComponentInChildren<MovementController>();
        while (player.CompareTag(playerTag) && lineOfSight.IsObjectInView(player)) {
            if (mc.IsRunning)
                suspicion += runWatchSuspicion * Time.deltaTime;

            yield return null;
        }
    }

    private void OnDrawGizmos() {
        if (!drawGizmos)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LosCheckDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, suspicionDistance);
        Gizmos.color = Color.white;
    }

}