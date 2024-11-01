using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool isClear = false;
    MainManager mainManager;
    DateTime gameStartTime;

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject lightBandit;
    [SerializeField] private GameObject heavyBandit;

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

    
    void Update() {
        if (!isClear && GetEnemyCount() == 0)
        {
            isClear = true;
            RecordRanking();
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
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
        return FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length;
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
}

[Serializable]
public class ClearTimeRecord
{
    public string playerName;
    public string clearTime;
    public ClearTimeRecord(string playerName, TimeSpan clearTime)
    {
        this.playerName = playerName;
        this.clearTime = clearTime.ToString();
    }
}

[Serializable]
public class ClearSpeedRanking
{ 
    public List<ClearTimeRecord> ranking;
    public ClearSpeedRanking()
    {
        ranking = new List<ClearTimeRecord>();
    }
    public void Add(ClearTimeRecord newRecord)
    {
        ranking.Add(newRecord);
        ranking = ranking
            .OrderBy(record => TimeSpan.Parse(record.clearTime))
            .Take(3)
            .ToList();
    }
}