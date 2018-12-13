using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void PlayGame ()
    {

        SceneManager.LoadScene("SampleScene");


    }

    public void BackToMenu ()
    {

        SceneManager.LoadScene("Menu");

    }

    public void QuitGame ()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
