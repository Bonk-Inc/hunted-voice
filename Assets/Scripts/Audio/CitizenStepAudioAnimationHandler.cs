using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenStepAudioAnimationHandler : MonoBehaviour {

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip[] stepSounds;

    public void Step() {
        var randomClip = stepSounds[Random.Range(0, stepSounds.Length)];
        source.clip = randomClip;
        source.Play();
    }

}