using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb2d;
    private SpriteRenderer render;
    private Animator anim;

    public bool isGrounded = false;
    private bool isDead = false;

    private bool jumpPress = false;
    private bool moveLeft = false;
    private bool moveRight = false;



    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        Move();
        Jump();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (moveLeft) horizontal = -1f;
        if (moveRight) horizontal = 1f;

        rb2d.velocity = new Vector2(horizontal * speed, rb2d.velocity.y);

        anim.SetBool("Run", horizontal != 0);
        if (horizontal != 0)
        {
            render.flipX = horizontal < 0;
        }

    }

    void Jump()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || jumpPress) && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
            isGrounded = false;
            jumpPress = false;
        }
        anim.SetBool("Jump", !isGrounded && rb2d.velocity.y > 0);
        anim.SetBool("Fall", !isGrounded && rb2d.velocity.y < 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            Die();
        }
        if (collision.CompareTag("Fruit"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.AddFruit(1);
        }
        if (collision.CompareTag("House"))
        {
            Debug.Log("Casa");
            Win();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box"))
        {
            isGrounded = false;
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        rb2d.velocity = Vector2.zero;
        anim.SetTrigger("Die");
        GetComponent<Collider2D>().enabled = false;

        GameManager.Instance.Die();

        // StartCoroutine(RestartLevel());
    }

    void Win()
    {
        if (GameManager.Instance.CheckFruits() <= 0)
        {
            GameManager.Instance.Win();
        }
    }

    public void PressJump()
    {
        if (!isGrounded) return;
        jumpPress = true;
    }

    public void PressLeft() => moveLeft = true;
    public void ReleaseLeft() => moveLeft = false;

    public void PressRight() => moveRight = true;
    public void ReleaseRight() => moveRight = false;
}
