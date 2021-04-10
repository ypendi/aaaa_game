using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Rotator rotatorScript;
    public Spawner spawnerScript;
    public Animator animator;
    private bool gameEnded = false;
    public bool soundOn = true;
    public static bool gameWon;
    private int maxLevelSceneBuildIndex = 21;

    private AudioListener audioListener;
    private GameObject aaaa_hit;
    public GameObject score;
    public GameObject pauseMenu;
    public GameObject soundToggle;
    //public Button clickBtn;


    private void Awake()
    {
        aaaa_hit = GameObject.Find("aaaa_hit");

        if (SceneManager.GetActiveScene().buildIndex == 0) // bad fix but bc if StartScreen is loaded, the sound toggle is on soundON
        {
            AudioListener.volume = 1;
            soundOn = true;
        }
    }

    private void Start()
    {

        if (PlayerPrefs.GetInt("maxLevel") > maxLevelSceneBuildIndex)
            PlayerPrefs.SetInt("maxLevel", maxLevelSceneBuildIndex);
    }

    private void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("maxLevel"));
        if (gameEnded && Input.GetMouseButtonDown(0))
        {
            RestartLevel();
        }
        if (gameWon)
        {
            LevelWon();
            if (Input.GetMouseButtonDown(0))
            {
                LoadNextLevel();
            }
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Pause();
        }
        
        if(AudioListener.volume == 0)
            soundOn = false;
        else
            soundOn = true;
        
    }

    public void SoundChange()
    {
        if (soundOn)
        {
            AudioListener.volume = 0;
        }

        else
        {
            AudioListener.volume = 1;
        }
    }

    public void LevelLost()
    {
        if (gameEnded)
            return;
        aaaa_hit.SetActive(false); //so it doesnt play both sounds a 
        gameEnded = true;
        animator.SetBool("gameOver", true);
        rotatorScript.enabled = false;
        spawnerScript.enabled = false;
        score.SetActive(false);
    }

    public void LevelWon()
    {
        int nextSceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;

        gameWon = false;
        animator.SetBool("gameWon", true);
        rotatorScript.enabled = false;
        spawnerScript.enabled = false;

        if(PlayerPrefs.GetInt("maxLevel") < nextSceneBuildIndex)
            PlayerPrefs.SetInt("maxLevel", nextSceneBuildIndex);
    }

    public void LoadStartScreen()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void LoadNextLevel()
    {
        gameWon = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StartGame()
    {
        if (PlayerPrefs.GetInt("maxLevel") > maxLevelSceneBuildIndex)
            PlayerPrefs.SetInt("maxLevel", maxLevelSceneBuildIndex);
        else if (PlayerPrefs.GetInt("maxLevel") <= 1)
            PlayerPrefs.SetInt("maxLevel", 2);

        SceneManager.LoadScene(PlayerPrefs.GetInt("maxLevel")); // Loads the first level that isn't "locked"
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void LevelLoad(int level)
    {
        SceneManager.LoadScene(level+1);
    }

    public void Pause()
    {
        Debug.Log("paused");
        pauseMenu.SetActive(true);
        //Time.timeScale = 0f;
        spawnerScript.enabled = false;
        rotatorScript.enabled = false;

        if (Input.GetKey(KeyCode.Escape))
            Resume();
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        //Time.timeScale = 1f;
        spawnerScript.enabled = true;
        rotatorScript.enabled = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
