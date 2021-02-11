using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{

    public float totalTime;

    public Text text;

    private float minutes;
    private float seconds;

    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject GameOverMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();

            }
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
            if (GameIsPaused == true)
            {
                LoadCurrentScene();
            }

        if (Input.GetKeyDown(KeyCode.Escape))
            if (GameIsPaused == true)
            {
                Application.Quit();

            }

        totalTime -= Time.deltaTime;
        minutes = (int)(totalTime / 60);
        seconds = (int)(totalTime % 60);

        text.text = minutes.ToString() + " : " + seconds.ToString();
    }

    void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;

    }

     void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
