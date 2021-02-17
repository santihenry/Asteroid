using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public static GameManager _Instance { get; private set; }

    private void Awake()
    {
        _Instance = this;
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



}
