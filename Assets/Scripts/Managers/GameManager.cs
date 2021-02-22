using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public GameObject PauseUI;
    public static bool gamePuse = false;



    private void Awake()
    {
        Instance = this;
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePuse)
                Resume();
            else
                Pause();
        }

    }


    public void Resume()
    {
        Time.timeScale = 1;
        gamePuse = false;
        PauseUI.SetActive(false);
    }

    void Pause()
    {
        Time.timeScale = 0;
        gamePuse = true;
        if(PauseUI != null)
        PauseUI.SetActive(true);
    }





    public void ExitGame()
    {
        Application.Quit();
    }
    public void LevelOne()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelOne");
    }
    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Lose");
    }

    public void Win()
    {
        SceneManager.LoadScene("Win");
    }


}
