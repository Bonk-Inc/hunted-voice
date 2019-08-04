using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegionUI : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI enteringText;

    [SerializeField]
    private string prefix = "Now entering: ";

    [SerializeField]
    private float screenTime;

    [SerializeField]
    private Animator animator;

    private Coroutine screenTimeCoroutine;
    private string animationKey = "InScreen";
    private Region currentPlayerRegion;
    private ObjectRegionInfo playerRegionInfo;

    private void Start() {
        PlayerSingleton.Instance.OnPlayerChanged += (oldPlayer, newPlayer) => UpdatePlayerRegionInfo();
        UpdatePlayerRegionInfo();
    }

    private void UpdatePlayerRegionInfo() {
        print("new Player");
        var regionInfo = PlayerSingleton.Instance.CurrentPlayer.GetComponent<ObjectRegionInfo>();
        if (regionInfo == playerRegionInfo)
            return;

        print("different Player");
        if (playerRegionInfo)
            playerRegionInfo.OnRegionChanged -= UpdateRegion;

        playerRegionInfo = regionInfo;

        if (playerRegionInfo)
            playerRegionInfo.OnRegionChanged += UpdateRegion;

        UpdateRegion(playerRegionInfo.Region);
    }

    private void UpdateRegion(ObjectRegionInfo regionInfo) {
        UpdateRegion(regionInfo.Region);
    }

    private void UpdateRegion(Region region) {
        if (currentPlayerRegion == region)
            return;

        animator.SetBool(animationKey, true);
        enteringText.text = $"{prefix}{region.RegionName}";
        if (screenTimeCoroutine != null)
            StopCoroutine(screenTimeCoroutine);

        screenTimeCoroutine = StartCoroutine(InScreenText());
    }

    private IEnumerator InScreenText() {
        yield return new WaitForSeconds(screenTime);
        animator.SetBool(animationKey, false);
    }

}