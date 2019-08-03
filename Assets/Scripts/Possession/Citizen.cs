using UnityEngine;
using TMPro;
using System.Collections;

public class Citizen : MonoBehaviour
{
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
    private const string animationBool = "InScreen";

    private void OnMouseEnter(){
        EnableInfo();
    }

    private void OnMouseExit(){
        StartCoroutine(DisableInfoRoutine());
    }

    private void EnableInfo(){
        print("yum");
        infoCanvas.enabled = true;
        infoFadeAnimator.SetBool(animationBool, true);

        nameText.text = characterName;
        ageText.text = "Age: " + characterAge.ToString();
    }

    private IEnumerator DisableInfoRoutine(){
        infoFadeAnimator.SetBool(animationBool, false);
        yield return new WaitForSeconds(0.5f);
        infoCanvas.enabled = false;
    }
}
