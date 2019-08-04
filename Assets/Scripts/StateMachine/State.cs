using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<EnumType> : MonoBehaviour
where EnumType : System.Enum {

    private StateMachine<EnumType> stateMachine;

    public abstract EnumType StateName { get; }
    public StateMachine<EnumType> StateMachine => stateMachine;

    public void InitState(StateMachine<EnumType> stateMachine) {
        this.stateMachine = stateMachine;
    }

    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void Reason() { }
    public virtual void LeaveState() { }

    public virtual void ChangeRegion(ObjectRegionInfo objectRegionInfo){}

}