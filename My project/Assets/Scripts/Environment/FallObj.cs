using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]

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
            StartCoroutine("FallPlatform");
        }


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
        if (gameObject.active == false) 
        {
            StopCoroutine("FallPlatform");
        }
    }




    IEnumerator FallPlatform () 
    {   
        StartCoroutine("BackPlatform");
        yield return new WaitForSeconds(_timeBeforeFall);
        rb.isKinematic = false;
        


    }

    IEnumerator BackPlatform () 
    {
        yield return new WaitForSeconds(_timeAfterFall + _timeBeforeFall);
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        moveBack = true;


    }
}


