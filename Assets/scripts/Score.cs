using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public Rotator rotatorScript;
    //public GameManager gameManagerScript;
    //public Text scoreText;
    public TextMeshProUGUI TMPscoreText;
    public static int score;

    void Start()
    {
        score = rotatorScript.pinsToWin;
    }

    private void Update()
    {
        //scoreText.text = score.ToString();
        TMPscoreText.text = score.ToString();

        if (score == 0)
        {
            GameManager.gameWon = true;
        }
    }

}