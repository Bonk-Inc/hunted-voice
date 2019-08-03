using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<EnumType> : MonoBehaviour
where EnumType : System.Enum {

    private Dictionary<EnumType, State<EnumType>> states = new Dictionary<EnumType, State<EnumType>>();

    public EnumType CurrentStateName { get; private set; }
    public State<EnumType> CurrentState { get; private set; }

    public void AddState(EnumType name, State<EnumType> state) {
        if (states.ContainsKey(name))
            RemoveState(name);

        states.Add(name, state);
        state.InitState(this);
    }

    public void RemoveState(EnumType name) {
        if (states.ContainsKey(name))
            return;

        states.Remove(name);
    }

    public void SetState(EnumType name) {
        if (CurrentState != null)
            CurrentState.LeaveState();

        if (!states.ContainsKey(name) || states[name] == null) {
            Debug.LogError($"State {name} isn't registered in this statemachine");
            CurrentState = null;
            return;
        }

        CurrentState = states[name];

        CurrentState.EnterState();
    }

    public void SetStateNull() {
        if (CurrentState != null)
            CurrentState.LeaveState();

        CurrentState = null;
    }

    private void Update() {
        UpdateCurrentState();
    }

    private void UpdateCurrentState() {
        if (CurrentState == null)
            return;

        CurrentState.UpdateState();
        CurrentState.Reason();
    }

}