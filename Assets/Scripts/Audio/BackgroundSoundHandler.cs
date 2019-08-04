using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BackGroundSounds {
    Normal,
    Chased,
    Choir,
    GameOver
}

public class BackgroundSoundHandler : MonoBehaviour {
    private static BackgroundSoundHandler instance;
    public static BackgroundSoundHandler Instance { get { return instance; } }

    [SerializeField]
    private AudioSource source;

    [SerializeField]
    private AudioClip[] backgroundSounds;

    private Dictionary<BackGroundSounds, AudioClip> soundsDict = new Dictionary<BackGroundSounds, AudioClip>();
    private BackGroundSounds currentBackgroundSound = BackGroundSounds.Normal;

    public BackGroundSounds CurrentBackgroundSound { get { return currentBackgroundSound; } }

    private void Awake() {
        if (instance != null) {
            Debug.LogError("Double singleton! removing!");
            Destroy(this);
        }
        instance = this;

        for (int i = 0; i < backgroundSounds.Length; i++) {
            soundsDict.Add((BackGroundSounds) i, backgroundSounds[i]);
        }
    }
    public void ChangeMusic(BackGroundSounds sound) {
        var nextClip = soundsDict[sound];
        currentBackgroundSound = sound;
        source.clip = nextClip;
        source.Play();
    }
}