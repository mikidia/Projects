using System.Collections;
using UnityEngine;

public class FallingThroughtPlatform : MonoBehaviour
{
    private Collider2D _collider;
    private bool _playerOnPlatform;
    void Start ()
    {
        _collider = GetComponent<Collider2D>();
    }


    void Update ()
    {
        if (_playerOnPlatform && Input.GetAxisRaw("Vertical") < 0)

        {
            _collider.enabled = false;
            StartCoroutine(EnableCollider());


        }
    }

    private IEnumerator EnableCollider ()
    {
        yield return new WaitForSeconds(1f);
        _collider.enabled = true;


    }
    private void SetPlayerOnPlatform (Collision2D other, bool value)
    {
        var player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            _playerOnPlatform = value;


        }
    }

    private void OnCollisionEnter2D (Collision2D other)
    {
        SetPlayerOnPlatform(other, true);
    }

    private void OnCollisionExit2D (Collision2D other)
    {

        SetPlayerOnPlatform(other, false);


    }



}
