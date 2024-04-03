using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NoDestroyManager : MonoBehaviour
{
    [SerializeField]GameObject[] Items;
    private void Awake ()
    {
        foreach (GameObject item in Items)

        {
            DontDestroyOnLoad(item);


        }
    }




}
