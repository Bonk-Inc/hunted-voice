using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PossessedState : CitizenState {
    public override CitizenStateType StateName => CitizenStateType.Possessed;

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private GameObject playerController;

    [SerializeField]
    private CitizenInfo npcInfo;

    public override void EnterState() {
        agent.enabled = false;
        npcInfo.IsNpc = false;
        playerController.SetActive(true);
    }

    public override void LeaveState() {
        playerController.SetActive(false);
        npcInfo.IsNpc = true;
        agent.enabled = true;
    }

}