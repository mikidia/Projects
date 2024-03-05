using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class FallObj : MonoBehaviour
{

    #region Declarations 
    [Header("Platform Settings")]

    Rigidbody2D rb;
    [SerializeField]float _timeBeforeFall;
    [SerializeField]float _fallTime;
    [SerializeField]float _platformSpeed;



    [SerializeField] float _timeAfterFall;
    Vector2 currentPossition;
    bool moveBack;

    #endregion
    #region MonoBehaviour
    private void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPossition = transform.position;
    }

    #endregion

    private void OnCollisionEnter2D (Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player)
        {   
            Invoke("FallPlatform", _timeBeforeFall);
        }


    }
    void FallPlatform ()
    {
        rb.isKinematic = false;
        
        Invoke("BackPlatform", _timeAfterFall);
    }
    void BackPlatform ()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        moveBack = true;
    }
    private void Update ()
    {
        if (moveBack)
        {
                transform.position = Vector2.MoveTowards(transform.position, currentPossition, _platformSpeed * Time.deltaTime);
            }
        if (transform.position.y == currentPossition.y)

        {
            moveBack = false;


        }
    }
}


