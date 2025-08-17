using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreTextMesh;
    public TextMeshProUGUI healthTextMesh;

    private void OnEnable()
    {
        PlayerController.OnPlayerDamaged += UpdateHealth;
        GameManager.OnScoreUpdated += UpdateScore;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDamaged -= UpdateHealth;
        GameManager.OnScoreUpdated -= UpdateScore;
    }

    private void Start()
    {
        if (PlayerController.GetPlayerHealth != null)
        {
            UpdateHealth(PlayerController.GetPlayerHealth());
        }
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
}
