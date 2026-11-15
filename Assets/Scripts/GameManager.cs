using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerWax playerWax;
    public TMP_Text progressText;
    public GameObject levelCompletePanel;
    public int totalEnemies = 3;
    public int totalBraziers = 2;
    public int enemiesAlive;

    int defeated, lit;
    Vector3 checkpoint;

    void Awake() { Instance = this; Time.timeScale = 1f; }

    void Start()
    {
        checkpoint = playerWax.transform.position;
        UpdateUI();
    }

    public void EnemyRegistered() { enemiesAlive++; UpdateUI(); }

    public void EnemyDefeated()
    {
        enemiesAlive--;
        defeated++;
        UpdateUI();
    }
    public void BrazierLit() { lit++; UpdateUI(); }
    public void SetCheckpoint(Vector3 pos) { checkpoint = pos; }

    public void Respawn()
    {
        playerWax.transform.position = checkpoint;
        playerWax.Heal(playerWax.maxWax);
    }

    public void LevelComplete()
    {
        levelCompletePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoadMenu() { SceneManager.LoadScene("MainMenu"); }

    void UpdateUI()
    {
        if (progressText == null) return;
        progressText.text = $"Phantoms left: {enemiesAlive}   Braziers: {lit}/{totalBraziers}";
    }
}