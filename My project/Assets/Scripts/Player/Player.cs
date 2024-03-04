using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Declarations

    [Header("Movement")]

    [SerializeField]int speed;
    [SerializeField]public float jumpHeihgt;
    [SerializeField]public float groundCheckDistance = 0;

    [NonSerialized] static bool jumpTriger;

    [Header("Settings")]

    [SerializeField]int _health =10;
    [SerializeField]public static float xInput;
    [SerializeField]public LayerMask whatIsGround;
    [SerializeField]public LayerMask whatIsPlatform;
    [SerializeField]public int _score=0;

    [NonSerialized]Rigidbody2D rigidBody;
    [NonSerialized]bool death = false;
    [NonSerialized]Vector2 facingDirection;
    [NonSerialized]Animator anim;
    [NonSerialized] GameManager gameManager;
    [NonSerialized]bool isStay;
    



    #endregion

    private void Awake ()
    {
        gameManager =GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

    }
    public void Update ()
    {
        if (death == false)
        {
            Jump();
            Attack();
            Movement();
            Debugs();
            IsGrounded();        }

    }


    public void Movement ()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (xInput >0) 
        {
            anim.SetInteger("MoveX", 1);
            

        }
        else if (xInput <0) 
        {
            anim.SetInteger("MoveX", -1);
            


        }
        else if (xInput ==0) 
        {
            anim.SetBool("IsStay", true);
        }

        if (xInput != 0)
        {


            rigidBody.velocity = new Vector2(xInput * speed, rigidBody.velocity.y);
            transform.localScale = new Vector2(2 * xInput, transform.localScale.y);
            anim.SetFloat("Side", transform.localScale.x);
            anim.SetBool("IsStay", false);


        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }

    }
    private void Debugs ()
    {

        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.red);
        Debug.DrawRay(transform.position, facingDirection * groundCheckDistance, Color.blue);
    }
    public bool IsGrounded ()

    {

        RaycastHit2D Groundhit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsPlatform);

        if (Groundhit || hit)
        {

            return true;

        }
        else
        {

            return false;

        }

    }
    public void Jump ()

    {

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() != false)
        {

            rigidBody.AddForce(Vector2.up * jumpHeihgt, ForceMode2D.Impulse);
            gameManager.Inverse();

        }
        else
        {

        }

    }
    public void GetDamage (int damage)
    {
        _health -= damage;

        //effect.Blood();

        anim.SetFloat("Hp", _health);
        if (_health <= 0)
        {

            anim.SetTrigger("Die");
            death = true;
        }
    }

    public void Attack ()
    {
        if (transform.localScale.x == 1) { facingDirection = Vector2.right; }


        else if (transform.localScale.x == -1) { facingDirection = Vector2.left; }




    }
    public void AddScore (int score)
    {
        _score += score;


    }
}
