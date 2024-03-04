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
    [SerializeField] int 
    [NonSerialized]bool inverse = false;

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
        changeGameSchene();
    }
    private void  changeGameSchene () 
    {
       if (inverse) 
        {
            light.intensity = minLight;

        }
        else 
        {
            light.intensity = maxLight; 
        
        }
    
    
    }


    IEnumerator SmoothChangeLightIntensivity () 
    {
        for (int i = 0; )
        yield return new WaitForSeconds(0.05f); 
    
    
    }





}
