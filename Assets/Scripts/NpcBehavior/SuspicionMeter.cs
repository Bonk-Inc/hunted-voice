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

    private void Start() {
        currentPlayer = PlayerSingleton.Instance.CurrentPlayer;

        PlayerSingleton.Instance.PlayerChanged.AddListener((newPlayer) => {
            currentPlayer = newPlayer;
        });
    }

    public void StartWatching() {
        print(name + " yeeehaaww");
        StartCoroutine(WatchForPlayer());
        StartCoroutine(WatchDistanceToPlayer());

        StartCoroutine(WatchSuspision());
    }

    public void StopWatching() {
        print(name + " yeeeh..  aahh");
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
        while (true) {
            if ((currentPlayer.transform.position - transform.position).magnitude < suspicionDistance)
                suspicion += distanceSuspicion * Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator WatchForPlayer() {
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