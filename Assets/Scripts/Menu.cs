using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{


    public void PlayGame()
    {
        
        SceneManager.LoadScene("mainScene");
        //GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Stop();
    }



    public void OptionsMenu()
    {
        SceneManager.LoadScene("Options");
        GameObject.FindGameObjectWithTag("Music").GetComponent<UnityEngine.AudioSource>().Play();

    }

    public void ControlsMenu()
    {
        SceneManager.LoadScene("Controls");
        GameObject.FindGameObjectWithTag("Music").GetComponent<UnityEngine.AudioSource>().Play();
    }

    public void CreditsMenu()
    {
        SceneManager.LoadScene("Credits");
        GameObject.FindGameObjectWithTag("Music").GetComponent<UnityEngine.AudioSource>().Play();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("StartScreen");

        //GameObject.FindGameObjectWithTag("Music").GetComponent<audio>().PlayMusic();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
