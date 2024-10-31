using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private PlayerAnimationController animController;
    private PlayerState playerState;
    private Rigidbody2D playerRB;
    private BoxCollider2D attackCollider;
    private GameManager gameManager;

    public string playerName { get; private set; }
    public float moveSpeed { get; private set; }
    public float playerScale { get; private set; }
    public float jumpPower { get; private set; }
    public int damage { get; private set; }
    void Start()
    {
        gameManager = GameManager.instance;
        playerName = gameManager != null ? gameManager.playerName : "Default Name";
        animController = new PlayerAnimationController(GetComponent<Animator>());
        playerState = new PlayerState();
        playerRB = GetComponent<Rigidbody2D>();
        attackCollider = GetComponent<BoxCollider2D>();
        playerScale = transform.localScale.x;
        jumpPower = 6;
        moveSpeed = 6;
        damage = 2;
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMove();
        Attack();
        Jump();
        Fall();
    }
    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!playerState.isAttacking && playerState.isOnGround)
            {
                animController.SetAttackTrigger();
                attackCollider.enabled = true;
                playerState.isAttacking = true;
            }
        }
    }
    private void Fall()
    {
        if (playerState.isOnGround == false)
        {
            animController.SetAirSpeed(playerRB.linearVelocityY);
        }
    }
    private void HorizontalMove()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0 && !playerState.isAttacking)//공격중일때는 움직이지 못함
        {
            SetViewDirection(horizontalInput);
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime * horizontalInput);
            animController.SetMovementState(1);
        }
        else
        {
            animController.SetMovementState(0);
        }
    }
    void SetViewDirection(float horizontalInput)
    {
        transform.localScale = new Vector3(horizontalInput > 0 ? playerScale : -playerScale, playerScale, playerScale);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && playerState.isOnGround && playerState.isAttacking == false)
        {
            playerState.isOnGround = false;
            animController.SetJumpTrigger();
            animController.SetGrounded(playerState.isOnGround);
            playerRB.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerState.isOnGround = true;
            animController.SetGrounded(playerState.isOnGround);
        }
    }
    public void AttackEnd()
    {
        attackCollider.enabled = false;
        playerState.isAttacking = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))  // 적 태그를 가진 오브젝트 확인
        {
            other.GetComponent<Enemy>().GetDamage(damage);  // 적에게 데미지 주기
        }
    }
}

public class PlayerAnimationController
{
    private Animator animator;
    public PlayerAnimationController(Animator animator)
    {
        this.animator = animator;
    }
    public void SetAttackTrigger() => animator.SetTrigger("Attack1");
    public void SetJumpTrigger() => animator.SetTrigger("Jump");
    public void SetGrounded(bool isOnGround) => animator.SetBool("Grounded", isOnGround);
    public void SetMovementState(int state) => animator.SetInteger("AnimState", state);
    public void SetAirSpeed(float speed) => animator.SetFloat("AirSpeedY", speed);
}

public class PlayerState
{
    public PlayerState()
    {
        isAttacking = false;
        isOnGround = false;
    }
    public bool isAttacking { get; set; }
    public bool isOnGround { get; set; }
}
