using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //public string startScreen;
    public GameObject pauseMenu;
    public bool isPaused;
    

    // Start is called before the first frame update
    void Start()
    {
        
        GameObject player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.Find("Player");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
                GameObject.FindGameObjectWithTag("Music").GetComponent<UnityEngine.AudioSource>().Stop();
                UnityEngine.AudioSource[] au = player.GetComponents<UnityEngine.AudioSource>();
                foreach(UnityEngine.AudioSource a in au)
                {
                    a.enabled = true;
                }
               
            }
            else
            {
                isPaused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                GameObject.FindGameObjectWithTag("Music").GetComponent<UnityEngine.AudioSource>().Play();
                UnityEngine.AudioSource[] au = player.GetComponents<UnityEngine.AudioSource>();
                foreach (UnityEngine.AudioSource a in au)
                {
                    a.enabled = false;
                }

            }
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("mainScene");
        ResumeGame();
        ScoreScript.scoreVal = 0;
    }

    public void ReturntoMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("startScreen");
    }

}