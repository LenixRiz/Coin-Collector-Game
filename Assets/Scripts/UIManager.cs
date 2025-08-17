using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTextMesh;
    [SerializeField] private TextMeshProUGUI healthTextMesh;
    [SerializeField] private TextMeshProUGUI TotalCoinLeftTextMesh;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    [SerializeField] private TextMeshProUGUI gameWonScoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject onPlayerGameOverUI;
    [SerializeField] private GameObject onPlayerWonUI;

    private void OnEnable()
    {
        PlayerController.OnHealthChanged += UpdateHealth;
        GameManager.OnScoreUpdated += UpdateScore;
        GameManager.CountAllCoinsInScene += UpdateTotalCoinLeft;
        GameManager.OnGameOver += OnPlayerGameOver;
        GameManager.OnGameWon += OnPlayerWon;
        restartButton.onClick.AddListener(RestartGame);
    }

    private void OnDisable()
    {
        PlayerController.OnHealthChanged -= UpdateHealth;
        GameManager.OnScoreUpdated -= UpdateScore;
        GameManager.CountAllCoinsInScene -= UpdateTotalCoinLeft;
        GameManager.OnGameOver -= OnPlayerGameOver;
        GameManager.OnGameWon -= OnPlayerWon;
        restartButton.onClick.RemoveListener(RestartGame);
    }

    private void UpdateScore(int score)
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
        TotalCoinLeftTextMesh.text = $"Total coins left: {totalCoin}";
    }

    public void RestartGame() //Made it public so it can be used as onClick in the UI inspector
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
