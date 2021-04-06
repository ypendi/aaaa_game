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
    public static bool gameWon;
    private int maxLevelSceneBuildIndex = 21;

    public GameObject score;
    public GameObject pauseMenu;
    //public Button clickBtn;

    private void Start()
    {
    
        if (PlayerPrefs.GetInt("maxLevel") > maxLevelSceneBuildIndex)
            PlayerPrefs.SetInt("maxLevel", maxLevelSceneBuildIndex);

        //if (Input.GetMouseButtonDown(0))
        //    Debug.Log("187");
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
    }

    public void LevelLost()
    {
        if (gameEnded)
            return;
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
