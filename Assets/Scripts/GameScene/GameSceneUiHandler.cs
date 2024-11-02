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
    [SerializeField] private GameObject wornPanel;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseDarkBackground;
    [SerializeField] private Button[] saveButtons;
    [SerializeField] private Button[] loadButtons;
    [SerializeField] private GameManager gameManager;

    private MainManager mainManager;
    bool isPaused;
    void Start()
    {
        mainManager = MainManager.instance;
        for (int i = 0; i < 3; ++i)
        {
            string savefileName = "savefile" + i;
            saveButtons[i].onClick.AddListener(() => mainManager.Save(savefileName, gameManager.GetPlayTime()));
            loadButtons[i].onClick.AddListener(() => mainManager.Load(savefileName));
        }
        InvokeRepeating("UpdatePlayTime", 0f, 1f);
        isPaused = false;
        if (mainManager != null)
        {
            playerNameText.text = mainManager.playerName;
        }
        else
        {
            playerNameText.text = "Default Name";
        }
    }

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
            TogleGameObject(pauseDarkBackground, true);
            TogleGameObject(pauseMenu, true);
            Time.timeScale = 0f;
        }
        else
        {
            TogleGameObject(pauseDarkBackground, false);
            TogleGameObject(pauseMenu, false);
            Time.timeScale = 1f;
        }
    }
    private void UpdatePlayTime()
    {
        TimeSpan playTime = gameManager.GetPlayTime();
        int minutes = playTime.Hours * 60 + playTime.Minutes;
        int seconds = playTime.Seconds;

        timerText.text = "" + (minutes < 10 ? "0" + minutes : minutes)
         + ":" + (seconds < 10 ? "0" + seconds : seconds);
    }
    public static void TogleGameObject(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
    public static void TogleGameObject(GameObject obj, bool activeState)
    {
        obj.SetActive(activeState);
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
    private void UpdateEnemyCount()
    {
        enemyCountText.text = "Enemy: " + gameManager.GetEnemyCount();
    }
}
