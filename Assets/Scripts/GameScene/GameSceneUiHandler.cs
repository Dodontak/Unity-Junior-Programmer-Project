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
    [SerializeField] private Button save1;
    [SerializeField] private Button save2;
    [SerializeField] private Button save3;
    [SerializeField] private Button load1;
    [SerializeField] private Button load2;
    [SerializeField] private Button load3;
    [SerializeField] private GameManager gameManager;
    private MainManager mainManager;
    bool isPaused;
    void Start()
    {
        mainManager = MainManager.instance;
        save1.onClick.AddListener(() => mainManager.Save("savefile1", gameManager.GetPlayTime()));
        save2.onClick.AddListener(() => mainManager.Save("savefile2", gameManager.GetPlayTime()));
        save3.onClick.AddListener(() => mainManager.Save("savefile3", gameManager.GetPlayTime()));
        load1.onClick.AddListener(() => mainManager.Load("savefile1"));
        load2.onClick.AddListener(() => mainManager.Load("savefile2"));
        load3.onClick.AddListener(() => mainManager.Load("savefile3"));
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
    public void TogleGameObject(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
    }
    private void TogleGameObject(GameObject obj, bool activeState)
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
