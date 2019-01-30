using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public void ToGame ()
    {
        SoundPlayer.instance.ButtonSound();
        SceneManager.LoadScene("Game");
    }

    public void ToTuto()
    {
        SoundPlayer.instance.ButtonSound();
        SceneManager.LoadScene("Tuto");
    }

    public void ToTitle()
    {
        SoundPlayer.instance.ButtonSound();
        SceneManager.LoadScene("Title");
    }
}
