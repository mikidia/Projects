using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallPlatformsManager : MonoBehaviour
{
    //[SerializeField]GameObject[] fallPlatforms;
    //[SerializeField] FallObj[] fallPlatforms;
    [SerializeField] Transform[] platformsCords;
    public static FallPlatformsManager instance;

    public void Awake ()
    {
        //fallPlatforms = GetComponentsInChildren<GameObject> (); 
        var fallPlatforms = GetComponentsInChildren<FallObj> ();
        platformsCords = new Transform[fallPlatforms.Length];
        //foreach (FallObj i  in fallPlatforms)
        //{
            


        //}
        //foreach (Transform i in platformsCords)
        //{
            




        //}
    }
    //public void ResetPlatformPosition () 
    //{
    //    //foreach (FallObj i in fallPlatforms) 
    //    //{

            
    //    //    i.gameObject.SetActive (true);
    //    //    i.ResetStartPos ();
    //    //    i.gameObject.SetActive(false);


    //    //}

    
    
    //}
}
