using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTriger : MonoBehaviour
{
    [SerializeField]Player player;
    private void OnTriggerEnter2D (Collider2D collision)
    {
        player = collision.gameObject.GetComponent<Player>();
        if ( player)
        {

            player.Death();
        }
    }
}
