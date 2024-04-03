using System;
using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using static UnityEditor.Searcher.SearcherWindow.Alignment;


public class Player : MonoBehaviour
{
    #region Declarations

    [Header("Movement")]
    [SerializeField]private bool _isControlEnable = true;
    [SerializeField]private float deathCdTmer = 0.3f;
    [SerializeField]private int speed;
    [SerializeField]private float jumpHeihgt;
    [SerializeField]private float groundCheckDistance = 0;
    [SerializeField]private Vector3 offset;

    [Header("Wall jump settings")]
    [SerializeField]private bool isWallSliding;
    [SerializeField]private float wallSlidingSpeed = 2f;
    [SerializeField]private bool isWallJumping;
    [SerializeField]private float wallJumpingDirection;
    [SerializeField]private float wallJumpingTime = 0.2f;
    [SerializeField]private float wallJumpingCounter;
    [SerializeField]private float wallJumpingDuration = 0.4f;
    [SerializeField]private float wallCheckDistance;
    [SerializeField]private Vector2 wallJumpingPower = new Vector2(8f, 16f);
    [SerializeField]private Transform wallCheck;
    [SerializeField]private LayerMask wallLayer;


    [Header("Jump Buffer Settings")]
    [SerializeField]private bool  isSpacePressed;
    [SerializeField]private bool isJumpBufferOn;
    [SerializeField]private float jumpBufferWaitTime;
    [SerializeField]private float groundBufferCheckDistance;
    [Range(0.01f,9999)]
    [SerializeField]private float bufferCdTime ;
    [SerializeField]private float timesPassed;
#if UNITY_EDITOR
    [SerializeField]private Vector2 facingDirection;
    [SerializeField]private bool jumpTriger;
#else
    [NonSerialized]Vector2 facingDirection;
    [NonSerialized]bool jumpTriger;

#endif
    [Header("Settings")]
    [SerializeField]static float xInput;
    [SerializeField]private LayerMask whatIsGround;
    [SerializeField]private LayerMask whatIsPlatform;
    [SerializeField]private bool isFalling = false;
    [SerializeField]private int _score=0;

#if UNITY_EDITOR
    [SerializeField]private Animator anim;
    [SerializeField]private GameManager gameManager;
    [SerializeField]private ParticleManager particleManager;
    [SerializeField]private Rigidbody2D rigidBody;
    [SerializeField]private FallPlatformsManager fallPlatformsManager;
    [SerializeField]private SoundManager soundManager;
    [SerializeField]private float _noGroundTime;
#else
    [NonSerialized]Animator anim;
    [NonSerialized]GameManager gameManager;
    [NonSerialized]ParticleManager particleManager;
    [NonSerialized]Rigidbody2D rigidBody;
    [NonSerialized]FallPlatformsManager fallPlatformsManager;
    [NonSerialized]private SoundManager soundManager;
    [NonSerialized]
    [NonSerialized]
    [NonSerialized]
    [NonSerialized]
    [NonSerialized]




#endif




    #endregion

    private void Awake ()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        gameManager = GameManager.instance;
        particleManager = ParticleManager.instance;
        fallPlatformsManager = FallPlatformsManager.instance;
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();








    }
    public void Update ()

    {


#if UNITY_EDITOR
        Debugs();
#endif
        if (_isControlEnable) { 
            Jump();
            Attack();
            Movement();
            WallSlide();
            WallJump();
        }
        Debug.Log(IsGrounded());


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
            if (_isControlEnable == false)
            {
                rigidBody.velocity = Vector2.zero;



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

        Debug.Log(facingDirection);
        Debug.DrawRay(transform.position, Vector2.right * facingDirection.x * wallCheckDistance, Color.white);

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
            if (!isFalling)
            {
                StartCoroutine(NoGround());
            }
            return false;



        }

    }
    bool IsWall ()
    {
        RaycastHit2D Wallhit = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, wallCheckDistance, wallLayer);
        if (Wallhit)
        {

            return true;

        }
        else
        {

            return false;

        }






    }

    bool IsBufferTime ()

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

    void Jump ()

    {

        if (Input.GetKeyDown (KeyCode.Space) && IsGrounded () != false && !isSpacePressed)
        {

            rigidBody.AddForce(Vector2.up * jumpHeihgt, ForceMode2D.Impulse);
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

    #region wall slide and jump 
    private bool IsWalled ()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
    private void WallSlide ()
    {
        if (IsWalled() && !IsGrounded() && xInput != 0f)
        {
            isWallSliding = true;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, Mathf.Clamp(rigidBody.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }
    private void WallJump ()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            gameManager.Inverse();
            isWallJumping = true;
            rigidBody.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                facingDirection = -facingDirection;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }


    private void StopWallJumping ()
    {
        isWallJumping = false;
    }


    #endregion

    public void Death ()
    {
        gameManager.Light();

        CheckpointTriger checkpoints =  GameObject.Find("checkpoint").GetComponent<CheckpointTriger>();

        transform.position = checkpoints.checkpoints[DataContainer.checkpointIndex].transform.position + new Vector3(0, 1f);
        StartCoroutine(DeathCd());
        fallPlatformsManager.ResetPlatformPosition();
        Uimanager uimanager = Uimanager.instance;
        uimanager.UpdateDeathCounter();
        particleManager.BloodParticles();
        rigidBody.velocity = Vector3.zero;





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
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
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
                
                break;
            }



        }
    }
    IEnumerator DeathCd ()
    {
        _isControlEnable = false;
        yield return new WaitForSeconds(deathCdTmer);
        _isControlEnable = true;




    }
    IEnumerator NoGround () 
    {
        isFalling = true;
        yield return new WaitForSeconds(_noGroundTime);
        
        
            soundManager.DeathSound();

        yield return new WaitForSeconds(_noGroundTime*5   );

        isFalling = false;







    }
}
