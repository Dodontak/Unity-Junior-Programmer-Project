using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float horizontalInput;
    public float speed;
    private Animator playerAnim;
    private float playerScale;
    public bool isOnGround;
    private bool isAttacking;
    public float jumpPower;
    private Rigidbody2D playerRB;
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerRB = GetComponent<Rigidbody2D>();
        playerScale = transform.localScale.x;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        playerAnim.SetInteger("AnimState", 0);
        if (horizontalInput != 0 && !isAttacking)
        {
            SetViewDirection(horizontalInput);
            transform.Translate(Vector2.right * speed * Time.deltaTime * horizontalInput);
            playerAnim.SetInteger("AnimState", 1);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (!isAttacking && isOnGround) {
                playerAnim.SetTrigger("Attack1");
                isAttacking = true;
            }
        }
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if (isOnGround == false)
        {
            playerAnim.SetFloat("AirSpeedY", playerRB.linearVelocityY);
        }
    }

    void SetViewDirection(float horizontalInput)
    {
        transform.localScale = new Vector3(horizontalInput > 0 ? playerScale : -playerScale, playerScale, playerScale);
    }

    void Jump()
    {
        if (isOnGround && isAttacking == false)
        {
            isOnGround = false;
            playerAnim.SetTrigger("Jump");
            playerAnim.SetBool("Grounded", false);
            playerRB.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            playerAnim.SetBool("Grounded", true);
        }
    }
    public void PrintFloat()
    {
        isAttacking = false;
    }
}
