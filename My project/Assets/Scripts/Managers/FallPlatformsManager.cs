using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallPlatformsManager : MonoBehaviour
{
    //[SerializeField]GameObject[] fallPlatforms;   
    [SerializeField] FallObj[] fallPlatforms;
    [SerializeField] Transform[] platformsCords;
    public static FallPlatformsManager instance;

    public void Awake ()
    {
        if (instance == null)
        {

            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
        //fallPlatforms = GetComponentsInChildren<GameObject> ();
        fallPlatforms = GetComponentsInChildren<FallObj> ();
        platformsCords = new Transform[fallPlatforms.Length];
        for (int i = 0; i < platformsCords.Length; i++) 
        {
            platformsCords[i] = fallPlatforms[i].transform;

        }

    }
    public void ResetPlatformPosition ()
    {
        int tempVar = 0;
        print("enter");
        foreach (FallObj i in fallPlatforms)
        {
            if (i.gameObject.active == false) 
            {
                i.gameObject.SetActive(true);
                i.ResetStartPos(new Vector2(platformsCords[tempVar].position.x, platformsCords[tempVar].position.y));
                i.gameObject.SetActive(false);

            }
            else
            {
                i.ResetStartPos(new Vector2(platformsCords[tempVar].position.x, platformsCords[tempVar].position.y));

            }


            tempVar++;  

        }



    }
}
