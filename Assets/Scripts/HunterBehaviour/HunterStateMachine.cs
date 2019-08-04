using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterStateMachine : StateMachine<HunterStateType>
{
    [SerializeField]
    private HunterState[] initialStates;

    [SerializeField]
    private HunterStateType initialState;

    protected void Start() {
        foreach (var state in initialStates) {
            AddState(state.StateName, state);
        }

        SetState(initialState);
    }
}
