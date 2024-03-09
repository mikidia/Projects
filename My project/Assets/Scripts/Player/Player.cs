using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;


public class Player : MonoBehaviour
{
    #region Declarations

    [Header("Movement")]
    [SerializeField]int speed;
    [SerializeField]float jumpHeihgt;
    [SerializeField]float groundCheckDistance = 0;
    [SerializeField]Vector3 offset;

    [Header("Jump Buffer Settings")]
    [SerializeField]bool  isSpacePressed;
    [SerializeField]bool isJumpBufferOn;
    [SerializeField]float jumpBufferWaitTime;
    [SerializeField]float groundBufferCheckDistance;
    [Range(0.01f,9999)]
    [SerializeField] float bufferCdTime ;
    [SerializeField]float timesPassed;

    [NonSerialized]Vector2 facingDirection;
    [NonSerialized]bool jumpTriger;

    [Header("Settings")]
    [SerializeField]int _health =10;
    [SerializeField]static float xInput;
    [SerializeField]LayerMask whatIsGround;
    [SerializeField]LayerMask whatIsPlatform;
    [SerializeField]int _score=0;


    [NonSerialized]bool death = false;
    [NonSerialized]Animator anim;
    [NonSerialized]GameManager gameManager;
    [NonSerialized]ParticleManager particleManager;
    [NonSerialized]FallObj fallPlatforms;
    [NonSerialized]Rigidbody2D rigidBody;





    #endregion

    private void Awake ()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        particleManager = GameObject.Find("ParticleManager").GetComponent<ParticleManager>();
        
        

    }
    public void Update ()
    {
        if (death == false)
        {
            Jump();
            Attack();
            Movement();
            Debugs();
            IsGrounded();
        }

    }


    public void Movement ()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (xInput > 0)
        {
            anim.SetInteger("MoveX", 1);


        }
        else if (xInput < 0)
        {
            anim.SetInteger("MoveX", -1);



        }
        else if (xInput == 0)
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
#if UNITY_EDITOR
    private void Debugs ()
    {

        Debug.DrawRay(transform.position - offset, Vector2.down * groundCheckDistance, Color.red);
        Debug.DrawRay(transform.position + offset, Vector2.down * groundCheckDistance, Color.red);

        Debug.DrawRay(transform.position - offset, Vector2.down * groundBufferCheckDistance, Color.yellow);
        Debug.DrawRay(transform.position + offset, Vector2.down * groundBufferCheckDistance, Color.yellow);

        Debug.DrawRay(transform.position, facingDirection * groundCheckDistance, Color.blue);

    }
#endif
    public bool IsGrounded ()

    {

        RaycastHit2D Groundhit = Physics2D.Raycast(transform.position-offset, Vector2.down, groundCheckDistance, whatIsGround);
        RaycastHit2D Groundhit2 = Physics2D.Raycast(transform.position+offset, Vector2.down, groundCheckDistance, whatIsGround);

        RaycastHit2D hit = Physics2D.Raycast(transform.position-offset, Vector2.down, groundCheckDistance, whatIsPlatform);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position+offset, Vector2.down, groundCheckDistance, whatIsPlatform);

        if (Groundhit || hit || hit2 || Groundhit2)
        {

            return true;

        }
        else
        {

            return false;

        }

    }
    public bool IsBufferTime ()

    {

        RaycastHit2D bufferGroundhit = Physics2D.Raycast(transform.position-offset, Vector2.down, groundBufferCheckDistance, whatIsGround);
        RaycastHit2D bufferGroundhit2 = Physics2D.Raycast(transform.position+offset, Vector2.down, groundBufferCheckDistance, whatIsGround);

        RaycastHit2D bufferhit = Physics2D.Raycast(transform.position-offset, Vector2.down, groundBufferCheckDistance, whatIsPlatform);
        RaycastHit2D bufferhit2 = Physics2D.Raycast(transform.position+offset, Vector2.down, groundBufferCheckDistance, whatIsPlatform);


        if (bufferGroundhit || bufferhit || bufferhit2 || bufferGroundhit2)
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

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() != false && !isSpacePressed)
        {

            rigidBody.AddForce(Vector2.up * jumpHeihgt, ForceMode2D.Impulse);
            gameManager.Switcher();
            gameManager.Inverse();

            if (isJumpBufferOn)
            {
                isSpacePressed = false;
            }

        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == false && !isSpacePressed && isJumpBufferOn)
        {
            isSpacePressed = true;
        }
        if (isSpacePressed && IsBufferTime() == true && isJumpBufferOn)
        {
            StartCoroutine("JumpBuffer");

        }

    }

    public void Death() 
    {
        
        CheckpointTriger checkpoints =  GameObject.Find("checkpoint").GetComponent<CheckpointTriger>();
        transform.position =  checkpoints.checkpoints[DataContainer.checkpointIndex].transform.position + new Vector3(0, 1f);
        Uimanager uimanager = GameObject.Find("UiManager").GetComponent<Uimanager>();
        gameManager.IsLight = true;
        
        uimanager.UpdateDeathCounter();
        particleManager.BloodParticles();





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
    IEnumerator JumpBuffer ()
    {
        while (true)
        {
            //Debug.Log(timesPassed);
            yield return new WaitForSeconds(jumpBufferWaitTime);
            if (bufferCdTime / jumpBufferWaitTime >= timesPassed)
            {
                if (IsGrounded() == true && isSpacePressed)
                {
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x,0);
                    rigidBody.AddForce(Vector2.up * jumpHeihgt, ForceMode2D.Impulse);
                    gameManager.Inverse();
                    isSpacePressed = false;
                    break;
                }
                timesPassed++;

            }
            else
            {
                isSpacePressed = false;
                timesPassed = 0;
                print("Break");
                break;
            }



        }
    }
}
