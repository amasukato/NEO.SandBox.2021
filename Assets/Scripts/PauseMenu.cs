using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    //Enemies Counter
    public GameObject[] enemies;
    public float totalTime;

    public Text CountdownText;
    public Text EnemyCountText;

    //Timer
    private float minutes;
    private float seconds;

    //UI
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    public GameObject GameOverMenuUI;
    public Image ChainActive;
    bool ChainIsActive;

    //Abilities
    public Image dashImage;
    public float cooldown1 = 0.5f;
    bool isCooldown = false;

    public Image SPbar;
    public float cooldown2 = 30f;
    bool isCooldown2 = false;



    private void Start()
    {
        dashImage.fillAmount = 0;
    }

    void Update()
    {
        DashCD();

        SPfiller();
        SPColorChanger();
        ChainIcon();

        if (Input.GetButtonDown("Pause"))
        {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();

            }
        }
        if (Input.GetButtonDown("Reset"))
            if (GameIsPaused == true)
            {
                LoadCurrentScene();
            }

        if (Input.GetKeyDown(KeyCode.Escape))
            if (GameIsPaused == true)
            {
                Application.Quit();

            }
        // Timer
        totalTime -= Time.deltaTime;
        minutes = (int)(totalTime / 60);
        seconds = (int)(totalTime % 60);

        CountdownText.text = minutes.ToString() + " : " + seconds.ToString();

        if (totalTime <= 0)
        {
            CountdownText.text = " Thank you for playing";
        }

        // Enemy Counter
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        EnemyCountText.text = enemies.Length.ToString() + " Enemies left ";
    }

    void ChainIcon()
    {
        if (Input.GetButtonDown("Left1"))
        {
            if (!ChainIsActive)
            {
                ChainIsActive = true;
                var ActiveColor = ChainActive.color;
                ActiveColor.a = 1f;
                ChainActive.color = ActiveColor; 
            }
            else
            {
                ChainIsActive = false;
                var ActiveColor = ChainActive.color;
                ActiveColor.a = 0f;
                ChainActive.color = ActiveColor;
            }
        }
    }
    void DashCD()
    {
        if (Input.GetButtonDown("Fire2") && isCooldown == false)
        {
            isCooldown = true;
            dashImage.fillAmount = 1;
        }

        if(isCooldown)
        {
            dashImage.fillAmount -= 1 / cooldown1 * Time.deltaTime;

            if (dashImage.fillAmount <= 0)
            {
                dashImage.fillAmount = 0;
                isCooldown = false;
            }
        }
    }

    void SPfiller()
    {
        SPbar.fillAmount += 1 / cooldown2 * Time.deltaTime;

        if (SPbar.fillAmount <= 0)
        {
            SPbar.fillAmount = 0;

        }

    }

    void SPColorChanger()
    {
        Color SPColor = Color.Lerp(Color.yellow, Color.blue, 0 / 1);

        SPbar.color = SPColor;
    }
    void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

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
        Time.timeScale = 1f;
    }
}
