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

    private bool _isFalling;


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
    private void OnCollisionExit2D (Collision2D collision)
    {
        if(_isFalling == false)
        {
            StopAllCoroutines();
        }

    }

    public  void ResetStartPos () 
    {
        StopAllCoroutines();
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        _isFalling = false;
        transform.position = currentPossition;

    }

    IEnumerator FallPlatform () 
    {   
        //StartCoroutine("BackPlatform");
        yield return new WaitForSeconds(_timeBeforeFall);
        rb.isKinematic = false;
        _isFalling = true;
        yield return new WaitForSeconds(_timeAfterFall);
        ResetStartPos();

    }


}


