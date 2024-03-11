using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;
using TMPro;
using System;


public class Uimanager : MonoBehaviour
{
    #region Declaration 
    [Header("Death counter")]
    [SerializeField] TMP_Text deathCounter;
    [SerializeField] int deaths;

    public static Uimanager instance;


    

    #endregion


    #region MonoBehaviour
    private void Awake ()
    {
        if (instance == null)
        {

            instance = this;

        }
        else 
        {
            Destroy(gameObject);
        }

        deathCounter = GameObject.Find("death numbers").GetComponent<TMP_Text>();

    }
    private void Start ()
    {

    }

    #endregion
    private void Update ()
    {

    }
    public void UpdateDeathCounter ()
    {
        deaths++;
        deathCounter.text = deaths.ToString();
    }
}
