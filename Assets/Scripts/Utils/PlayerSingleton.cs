using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerChangedEvent : UnityEvent<GameObject> { }

public class PlayerSingleton : MonoBehaviour {

    [SerializeField]
    private GameObject currentPlayer;
    private static PlayerSingleton instance;

    public GameObject CurrentPlayer => currentPlayer;
    public static PlayerSingleton Instance => instance;

    public PlayerChangedEvent PlayerChanged;

    private void Awake() {
        if (instance != null) {
            Debug.LogError("Double singleton! removing!");
            Destroy(this);
        }
        instance = this;
    }

    public void SetPlayer(GameObject newPlayer) {
        var statemachine = currentPlayer.GetComponent<CitizenStateMachine>();
        statemachine.SetState(CitizenStateType.Patrolling);

        currentPlayer = newPlayer;
        PlayerChanged?.Invoke(newPlayer);
    }

}