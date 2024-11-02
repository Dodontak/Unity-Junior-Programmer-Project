using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    MainManager mainManager;
    DateTime gameStartTime;
    bool isGameOver = false;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject lightBandit;
    [SerializeField] private GameObject heavyBandit;
    [SerializeField] private GameObject stageClearPanel;

    void Awake()
    {
        mainManager = MainManager.instance;
        SaveData saveData = mainManager.GetSaveData(mainManager.saveName);
        gameStartTime = DateTime.Now;
        if (mainManager.saveName != "default")
        {
            mainManager.playerName = saveData.playerData.name;
            gameStartTime -= TimeSpan.Parse(saveData.playTime);
        }
        SpawnEnemies(saveData.enemiesData);
        SpawnPlayer(saveData.playerData);
    }

    
    void SpawnEnemies(List<EnemyData> enemiesData)
    {
        foreach (EnemyData enemyData in enemiesData)
        {
            if (enemyData.enemyName == "Heavy Bandit")
            {
                Instantiate(heavyBandit, enemyData.position, heavyBandit.transform.rotation);
            }
            else if (enemyData.enemyName == "Light Bandit")
            {
                Instantiate(lightBandit, enemyData.position, lightBandit.transform.rotation);
            }
        }
    }

    void SpawnPlayer(PlayerData playerData)
    {
        Instantiate(playerPrefab, playerData.position, playerPrefab.transform.rotation);
    }

    public TimeSpan GetPlayTime()
    {
        return DateTime.Now - gameStartTime;
    }

    public int GetEnemyCount()
    {
        int count = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
        if (isGameOver == false && count == 0)
        {
            isGameOver = true;
            GameClear();
        }
        return count;
    }

    void RecordRanking()
    {
        string recordFile = Application.persistentDataPath + "/ranking.json";
        ClearSpeedRanking ranking = new ClearSpeedRanking();
        if (File.Exists(recordFile))
        {
            string rankingRecordJson = File.ReadAllText(recordFile);
            ranking = JsonUtility.FromJson<ClearSpeedRanking>(rankingRecordJson);
        }
        ClearTimeRecord newRecord = new ClearTimeRecord(mainManager.playerName, GetPlayTime());
        ranking.Add(newRecord);
        string json = JsonUtility.ToJson(ranking);
        File.WriteAllText(recordFile, json);
    }
    public void GameClear()
    {
        GameSceneUiHandler.TogleGameObject(stageClearPanel, true);
        RecordRanking();
        Time.timeScale = 1f;
        StartCoroutine(WaitAndSwithScene(0, 5f));
    }
    IEnumerator WaitAndSwithScene(int sceneIdx, float sec)
    {
        yield return new WaitForSeconds(sec);
        SceneManager.LoadScene(sceneIdx);
    }
}
