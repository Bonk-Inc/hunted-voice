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

    private string playerTag = "Player";
    private string startTag = string.Empty;

    public override void EnterState() {
        startTag = StateMachine.gameObject.tag;
        StateMachine.gameObject.tag = playerTag;
        agent.enabled = false;
        npcInfo.IsNpc = false;
        playerController.SetActive(true);
    }

    public override void LeaveState() {
        StateMachine.gameObject.tag = startTag;
        playerController.SetActive(false);
        npcInfo.IsNpc = true;
        agent.enabled = true;
    }

}