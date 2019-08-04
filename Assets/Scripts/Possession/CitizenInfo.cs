using System.Collections;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField]
    private float minPossessionTime = 1f;
    [SerializeField]
    private float maxPossessionDistance = 3f;

    [SerializeField]
    private Canvas possessionCanvas;
    [SerializeField]
    private Animator demonIcon;
    [SerializeField]
    private Image percentageBar;
    private const string animationBool = "InScreen";

    public bool IsNpc { get; set; } = true;
    public bool InfoOpen { get; private set; } = false;
    public bool IsBeingPossessed { get; private set; } = false;

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
        if (!IsNpc)
            return;
        if (CalculateDistance(this.gameObject, PlayerSingleton.Instance.CurrentPlayer) <= maxPossessionDistance)
            StartCoroutine(PossessionCoroutine());
        StartCoroutine(DisableInfoRoutine());
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

    private IEnumerator PossessionCoroutine() {
        var previousSound = BackgroundSoundHandler.Instance.CurrentBackgroundSound;
        ToggleVisuals(true);
        IsBeingPossessed = true;
        float timeLeft = minPossessionTime;
        bool possessionFinished = false;
        while (Input.GetMouseButton(0) && !possessionFinished) {
            float percentage = 1 / minPossessionTime * timeLeft;
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0) {
                percentage = 0;
                possessionFinished = true;
            }
            percentageBar.fillAmount = 1 - percentage;

            yield return null;
        }
        if (possessionFinished) {
            stateMachine.SetState(CitizenStateType.Possessed);
            PlayerSingleton.Instance.SetPlayer(this.gameObject);
            yield return new WaitForSeconds(2f);
        }
        IsBeingPossessed = false;
        ToggleVisuals(false, previousSound);
    }

    private void ToggleVisuals(bool activate, BackGroundSounds sound = BackGroundSounds.Choir) {
        possessionCanvas.enabled = activate;
        BackgroundSoundHandler.Instance.ChangeMusic(sound);
    }

    private float CalculateDistance(GameObject currentObject, GameObject otherObject) {
        float distance = (currentObject.transform.position - otherObject.transform.position).magnitude;
        return distance;
    }
}