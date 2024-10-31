using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneUiHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI enemyCountText;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button saveButton;

    private GameManager gameManager;
    DateTime currentTime;
    bool isPaused;
    void Start()
    {
        gameManager = GameManager.instance;
        currentTime = DateTime.Now;
        saveButton.onClick.AddListener(gameManager.Save);
        InvokeRepeating("UpdatePlayTime", 0f, 1f);
        isPaused = false;
        if (gameManager != null)
        {
            playerNameText.text = gameManager.playerName;
        }
        else 
        {
            playerNameText.text = "Default Name";
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemyCount();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void Pause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    private void UpdatePlayTime()
    {
        TimeSpan playTime = GetPlayTime();
        int minutes = playTime.Hours * 60 + playTime.Minutes;
        int seconds = playTime.Seconds;

        timerText.text = "" + (minutes < 10 ? "0" + minutes : minutes)
         + ":" + (seconds < 10 ? "0" + seconds : seconds);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
    private void UpdateEnemyCount()
    {
        enemyCountText.text = "Enemy: " + GetEnemyCount();
    }
    private TimeSpan GetPlayTime()
    {
        return DateTime.Now - currentTime;
    }
    private int GetEnemyCount()
    {
        return FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
    }
}
