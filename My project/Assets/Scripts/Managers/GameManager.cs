using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    #region Declarations 
    [Header("Light settings")]
    [SerializeField]float minLight;
    [SerializeField] float maxLight;
    [SerializeField]Light2D light;
    [SerializeField]bool inverse = false;

    #endregion

    public void Awake ()
    {
        light =  GameObject.Find("Light 2D").GetComponent<Light2D>();

    }
    public void Update ()
    {
       
    }


    public void Inverse () {
        inverse = !inverse;
        StartCoroutine("SmoothChangeLightIntensivity");
    }



    IEnumerator SmoothChangeLightIntensivity () 
    {
        for (int i = 0; i<=3; i++ )
        {
            if (inverse && light.intensity >=0.1f)
            {
                light.intensity -= (maxLight-minLight)/4;

            }
            else if (!inverse && light.intensity <0.99f)
            {
                light.intensity += (maxLight-minLight)/4;

            }
            yield return new WaitForSeconds(0.1f);



        }


    }





}
