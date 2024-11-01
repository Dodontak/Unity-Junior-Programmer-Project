using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    MainManager mainManager;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject lightBandit;
    [SerializeField] private GameObject heavyBandit;
    void Awake()
    {
        mainManager = MainManager.instance;
        SaveData saveData = mainManager.GetSaveData(mainManager.saveName);
        mainManager.playerName = saveData.playerData.name;
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
}
