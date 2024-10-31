using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    static public GameManager instance;
    public string playerName;
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

    public void Save()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        Player player = FindAnyObjectByType<Player>();
        SaveData saveData = new SaveData();
        for (int i = 0; i < enemies.Length; i++)
        {
            saveData.enemiesData.Add(GetEnemyData(enemies[i]));
        }
        saveData.playerData = GetPlayerData(player);
        string json = JsonUtility.ToJson(saveData);

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

    public void Load()
    {

    }
}
