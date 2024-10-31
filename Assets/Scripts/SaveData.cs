using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
class SaveData
{
    public PlayerData playerData;
    public List<EnemyData> enemiesData;
    public SaveData()
    {
        playerData = new PlayerData();
        enemiesData = new List<EnemyData>();
    }
}

[Serializable]
public class EnemyData
{
    public string enemyName;
    public Vector3 position;
    public int health;
    public float speed;
    public int attackPoint;
    public float enemyScale;
}

[Serializable]
public class PlayerData
{
    public string name;
    public Vector3 position;
    public float moveSpeed;
    public float playerScale;
    public float jumpPower;
    public int damage;
}