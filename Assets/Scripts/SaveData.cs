using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public PlayerData playerData;
    public List<EnemyData> enemiesData;
    public string playTime;
    public SaveData()
    {
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