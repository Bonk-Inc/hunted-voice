using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenStateMachine : StateMachine<CitizenStateType> {

    [SerializeField]
    private CitizenState[] initialStates;

    [SerializeField]
    private CitizenStateType initialState;

    protected void Awake() {
        foreach (var state in initialStates) {
            AddState(state.StateName, state);
        }

        SetState(initialState);
    }

}