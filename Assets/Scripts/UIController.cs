using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text finalScoreText;

    public GameObject hudPanel;
    public GameObject startPanel;
    public GameObject gameOverPanel;
    public GameController gameController;

    private void Start()
    {
        startPanel.SetActive(true);

    }

    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void PressStart()
    {
        if (!gameController.firstRound)
        {
            gameController.ResetScene();
        }
        gameController.StartGame();
        startPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        SetScore(0);
        hudPanel.SetActive(true);
    }

    public void ShowGameOverScreen(int finalScore)
    {
        hudPanel.SetActive(false);
        finalScoreText.text = "Your Score is: " + finalScore.ToString();
        gameOverPanel.SetActive(true);
    }
}
