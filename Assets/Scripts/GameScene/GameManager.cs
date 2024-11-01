using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    MainManager mainManager;
    [SerializeField] private GameObject lightBandit;
    [SerializeField] private GameObject heavyBandit;
    void Awake()
    {
        mainManager = MainManager.instance;
        SaveData saveData = mainManager.GetSaveData(mainManager.saveName);
        if (mainManager.saveName != "default")
        {
            mainManager.playerName = saveData.playerData.name;
        }
        foreach (EnemyData enemyData in saveData.enemiesData)
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
}
