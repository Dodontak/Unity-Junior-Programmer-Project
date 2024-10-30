using UnityEngine;
using System.Collections;

public class LightBandit : Enemy
{
    private LightBanditState state;
    private LightBanditAnimationController animator;
    void Start()
    {
        enemyName = "Light Bandit";
        speed = 2;
        health = 3;
        attackPoint = 1;
        state = new LightBanditState();
        animator = new LightBanditAnimationController(GetComponent<Animator>());
        enemyScale = transform.localScale.x;
        StartCoroutine(MoveRandomly());
        StartCoroutine(ChangeViewDirectionRandomly());
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    public override void Move()
    {
        if (state.isAlive && state.isMoving)
        {
            SetViewDirection(state.facingRight);
            transform.Translate(Vector2.right * speed * Time.deltaTime * state.facingRight);
        }
    }
    public override void Attack()
    {

    }
    public override void GetDamage(int val)
    {
        health -= val;
        if (health > 0)
        {
            animator.SetHurtTrigger();
        }
        else
        {
            animator.SetDeathTrigger();
            state.isAlive = false;
            Die();
        }
    }
    public override void Die()
    {
        Destroy(gameObject, 2f);
    }
    IEnumerator MoveRandomly()
    {
        while (state.isAlive)
        {
            float sec = Random.Range(1f, 2f);
            state.isMoving = Random.Range(0, 3) == 0;
            if (state.isMoving)
            {
                animator.SetMovementState(2);
            }
            yield return new WaitForSeconds(sec);
            state.isMoving = false;
            animator.SetMovementState(0);
        }
    }

    void SetViewDirection(int viewDirection)
    {
        transform.localScale = new Vector3(viewDirection < 0 ? enemyScale : -enemyScale, enemyScale, enemyScale);
    }

    IEnumerator ChangeViewDirectionRandomly()
    {
        while (state.isAlive)
        {
            float sec = Random.Range(0.5f, 2f);
            int random = Random.Range(0, 2);
            yield return new WaitForSeconds(sec);
            state.facingRight = random == 1 ? -1 : 1;
        }
    }
}


public class LightBanditAnimationController
{
    private Animator animator;
    public LightBanditAnimationController(Animator animator)
    {
        this.animator = animator;
    }
    public void SetAttackTrigger() => animator.SetTrigger("Attack1");
    public void SetJumpTrigger() => animator.SetTrigger("Jump");
    public void SetHurtTrigger() => animator.SetTrigger("Hurt");
    public void SetDeathTrigger() => animator.SetTrigger("Death");
    public void SetGrounded(bool isOnGround) => animator.SetBool("Grounded", isOnGround);
    public void SetMovementState(int state) => animator.SetInteger("AnimState", state);
    public void SetAirSpeed(float speed) => animator.SetFloat("AirSpeedY", speed);
}

public class LightBanditState
{
    public bool isAlive { get; set; }
    public bool isAttacking { get; set; }
    public bool isMoving { get; set; }
    public int facingRight { get; set; }
    public LightBanditState()
    {
        isAlive = true; 
        isAttacking = false;
        isMoving = false;
        facingRight = 1;
    }
}