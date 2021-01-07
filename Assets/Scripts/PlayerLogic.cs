using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public Rigidbody2D rb;
    public int health;
    public float movementSpeed, faceDirection, jumpForce;
    public bool isGrounded, isDead,isRunning,isAttacking;
    public Animator anim;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == false)
        {
            Movement();
            Jump();
            Attack();
            MouseClickKill();
            Animations();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            isRunning = false;
            isGrounded = false;
        }
    }
    public void Jump()
    {
        if (isAttacking == true) { return; }

        if (isGrounded == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
               
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }
    public void Movement()
    {
        if(isAttacking == true) { return; }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * movementSpeed, rb.velocity.y);
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), transform.localScale.y, transform.localScale.z);
        }

        if (rb.velocity.x != 0 && isGrounded == true)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    public void MouseClickKill()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3)), Vector2.zero);

            if (hit.collider != null)
            {
                if(hit.collider.tag == "Player")
                {
                    Debug.Log("hit palyer");
                    KillPlayer();
                }
            }

        }
    }
    public void Animations()
    {
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isGrounded", isGrounded);
    }
    public void KillPlayer()
    {
        isDead = true;
        anim.SetTrigger("Dead");
    }
    public void Attack()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("attack!");
            anim.SetInteger("Attack", Random.Range(1, 4));
            isAttacking = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isAttacking = false;
            anim.SetInteger("Attack", 0);
        }
    }
}
