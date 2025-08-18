using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTextMesh;
    [SerializeField] private TextMeshProUGUI healthTextMesh;
    [SerializeField] private TextMeshProUGUI totalCoinLeftTextMesh;
    [SerializeField] private TextMeshProUGUI speedTextMesh;
    [SerializeField] private TextMeshProUGUI coinTextMesh;
    [SerializeField] private TextMeshProUGUI highScoreTextMesh;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    [SerializeField] private TextMeshProUGUI gameWonScoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject onPlayerGameOverUI;
    [SerializeField] private GameObject onPlayerWonUI;

    private void OnEnable()
    {
        PlayerController.OnHealthChanged += UpdateHealth;
        GameManager.OnScoreUpdated += UpdateScore;
        GameManager.OnGameOver += OnPlayerGameOver;
        GameManager.OnGameWon += OnPlayerWon;
        GameManager.OnTotalCoinUpdated += UpdateTotalCoinLeft;
        GameManager.OnHighscoreUpdated += UpdateHighScore;
        PlayerController.OnPowerUpStatusUpdated += UpdatePowerUpStatus;
        Coin.OnPowerUpStatusUpdated += UpdatePowerUpStatus;
        restartButton.onClick.AddListener(RestartGame);
    }

    private void OnDisable()
    {
        PlayerController.OnHealthChanged -= UpdateHealth;
        GameManager.OnScoreUpdated -= UpdateScore;
        GameManager.OnGameOver -= OnPlayerGameOver;
        GameManager.OnGameWon -= OnPlayerWon;
        GameManager.OnTotalCoinUpdated -= UpdateTotalCoinLeft;
        GameManager.OnHighscoreUpdated -= UpdateHighScore;
        PlayerController.OnPowerUpStatusUpdated -= UpdatePowerUpStatus;
        Coin.OnPowerUpStatusUpdated -= UpdatePowerUpStatus;
        restartButton.onClick.RemoveListener(RestartGame);
    }

    private void UpdateScore(float score)
    {
        scoreTextMesh.text = $"Score: {score}";
        Debug.Log("Score updated");
    }

    private void UpdateHealth(float health)
    {
        healthTextMesh.text = $"Health: {health}";
    }

    private void UpdateTotalCoinLeft(int totalCoin)
    {
        totalCoinLeftTextMesh.text = $"Total coins left: {totalCoin}";
    }

    private void UpdatePowerUpStatus(string powerUpName, float timeLeft)
    {
        // This can be expanded for other power-ups like the coin boost
        if (powerUpName == "Speed")
        {
            if (timeLeft > 0)
            {
                speedTextMesh.text = $"Speed Boost: {timeLeft:F0}s";
                speedTextMesh.gameObject.SetActive(true);
            }
            else
            {
                speedTextMesh.gameObject.SetActive(false);
            }
        }
        else if (powerUpName == "Coin")
        {
            if (timeLeft > 0)
            {
                coinTextMesh.text = $"Coin Boost: {timeLeft:F0}s"; //F0 means there will be no number behind comma
                coinTextMesh.gameObject.SetActive(true);
            }
            else
            {
                coinTextMesh.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateHighScore(string text, int highScore)
    {
        highScoreTextMesh.text = $"{text} {highScore}";
    }

    public void RestartGame() //Made it public so it can be used as onClick in the UI inspector
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log($"Highscore from PlayerPrefs: {PlayerPrefs.GetInt("HighScore")}");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    
    //Reusable code for showing the end screen
    private void ShowEndScreen(GameObject screenToShow, TextMeshProUGUI totalScore, int finalScore)
    {
        screenToShow.SetActive(true);
        totalScore.text = $"Final score: {finalScore}";
        Time.timeScale = 0f;
    }

    private void OnPlayerGameOver(int finalScore)
    {
        ShowEndScreen(onPlayerGameOverUI, gameOverScoreText, finalScore);
    }


    private void OnPlayerWon(int finalScore)
    {
        ShowEndScreen(onPlayerWonUI, gameWonScoreText, finalScore);
    }   
}
