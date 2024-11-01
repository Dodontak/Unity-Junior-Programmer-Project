using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string enemyName {get; set;}
    public int health {get; set;}
    public float speed {get; set;}
    public int attackPoint {get; set;}
    public float enemyScale {get; set;}
    public EnemyState state;
    public abstract void Move();
    public abstract void Attack();
    public abstract void GetDamage(int val);
    public abstract void Die();
}

public class EnemyState
{
    public bool isAlive { get; set; }
    public bool isAttacking { get; set; }
    public bool isMoving { get; set; }
    public int facingRight { get; set; }
    public EnemyState()
    {
        isAlive = true; 
        isAttacking = false;
        isMoving = false;
        facingRight = Random.Range(0, 2) == 0 ? -1 : 1;
    }
}