using System.Collections;
using UnityEngine;
using TMPro;

public class CitizenInfo : MonoBehaviour {
    [SerializeField, Header("Character Info")]
    private string characterName;
    [SerializeField]
    private int characterAge;

    [SerializeField, Header("UI Objects")]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI ageText;
    [SerializeField]
    private TextMeshProUGUI convertedText;

    [SerializeField, Header("Animation Objects")]
    private Animator infoFadeAnimator;
    [SerializeField]
    private Canvas infoCanvas;

    [SerializeField, Header("Possession")]
    private CitizenStateMachine stateMachine;
    private const string animationBool = "InScreen";

    public bool IsNpc { get; set; } = true;
    public bool InfoOpen { get; private set; } = false;

    private void OnMouseEnter() {
        if (!IsNpc)
            return;

        EnableInfo();
    }

    private void OnMouseExit() {
        if (!IsNpc)
            return;

        StartCoroutine(DisableInfoRoutine());
    }

    private void OnMouseDown() {
        if (!IsNpc || !InfoOpen)
            return;

        StartCoroutine(DisableInfoRoutine());
        stateMachine.SetState(CitizenStateType.Possessed);
        PlayerSingleton.Instance.SetPlayer(this.gameObject);
    }

    private void EnableInfo() {
        InfoOpen = true;
        infoCanvas.enabled = true;
        infoFadeAnimator.SetBool(animationBool, true);

        nameText.text = characterName;
        ageText.text = "Age: " + characterAge.ToString();
    }

    private IEnumerator DisableInfoRoutine() {
        InfoOpen = false;
        infoFadeAnimator.SetBool(animationBool, false);
        yield return new WaitForSeconds(0.5f);
        infoCanvas.enabled = false;
    }
}