using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sppedk : MonoBehaviour
{
    [SerializeField] GameObject dialog;
    public void OnTriggerEnter2D (Collider2D collision)
    {
        Player player =collision.GetComponent<Player>();
        if (player) 
        {
        dialog.SetActive(true);



        }
    }
    public void OnTriggerExit2D (Collider2D collision)
    {
        dialog.SetActive(false);

    }
}
