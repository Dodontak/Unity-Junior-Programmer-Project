using UnityEngine;
using System.IO;
using System.Data.Common;
using UnityEngine.SceneManagement;
using System;

public class MainManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    static public MainManager instance;
    public string playerName;
    public string saveName;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save(string saveFileName, TimeSpan playTime)
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        Player player = FindAnyObjectByType<Player>();
        SaveData saveData = new SaveData();
        for (int i = 0; i < enemies.Length; i++)
        {
            saveData.enemiesData.Add(GetEnemyData(enemies[i]));
        }
        saveData.playerData = GetPlayerData(player);
        saveData.playTime = playTime.ToString();
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(Application.persistentDataPath + "/" + saveFileName + ".json", json);
    }
    EnemyData GetEnemyData(Enemy enemy)
    {
        EnemyData enemyData = new EnemyData();
        enemyData.enemyName = enemy.enemyName;
        enemyData.position = enemy.transform.position;
        enemyData.health = enemy.health;
        enemyData.speed = enemy.speed;
        enemyData.attackPoint = enemy.attackPoint;
        enemyData.enemyScale = enemy.enemyScale;
        return enemyData;
    }

    PlayerData GetPlayerData(Player player)
    {
        PlayerData playerData = new PlayerData();
        playerData.name = player.playerName;
        playerData.position = player.transform.position;
        playerData.moveSpeed = player.moveSpeed;
        playerData.playerScale = player.playerScale;
        playerData.jumpPower = player.jumpPower;
        playerData.damage = player.damage;
        return playerData;
    }
    public SaveData GetSaveData(string saveName)
    {
        string path = Application.persistentDataPath + "/" + saveName + ".json";
        SaveData data = new SaveData();
        if (saveName == "default")
        {
            TextAsset defaultSave = Resources.Load<TextAsset>("default");
            data = JsonUtility.FromJson<SaveData>(defaultSave.text);
        }
        else
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                data = JsonUtility.FromJson<SaveData>(json);
            }
        }
        return data;
    }
    public void Load(string saveFileName)
    {
        saveName = saveFileName;
        string path = Application.persistentDataPath + "/" + saveName + ".json";
        if (File.Exists(path))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log(saveName + " Does not exist.");
        }
    }
}
