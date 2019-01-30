using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {

    [HideInInspector]
    public static SoundPlayer instance; // pour le singleton pattern

    private AudioSource[] sounds;

    private void Awake()
    {
        Screen.SetResolution(1024, 768, false); // forcer une résolution adaptée au jeu

        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        // singleton pattern, créer une seule instance du SoundPlayer
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        sounds = GetComponents<AudioSource>();
    }

    public void ButtonSound()
    {
        sounds[0].Play();
    }

    public void StopMenuMusic()
    {
        sounds[1].Stop();
    }

    public void PlayMenuMusic()
    {
        sounds[1].Play();
    }
}
