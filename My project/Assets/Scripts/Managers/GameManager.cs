
using System.Collections;

using UnityEngine;

using UnityEngine.Rendering.Universal;


public class GameManager : MonoBehaviour
{
    #region Declarations 
    [Header("Light settings")]
    [SerializeField]float minLight;
    [SerializeField]float maxLight;
    [SerializeField]Light2D light;
    [SerializeField]bool inverse;
	[SerializeField]bool isLight= true;
    [SerializeField] GameObject[] lightObjects;
    [SerializeField] GameObject[] darkObjects;




    #region GetersSeters
    public bool IsLight { get => isLight; set => isLight = value; }
    
    #endregion





    #endregion

    public void Awake ()
    {
        lightObjects = GameObject.FindGameObjectsWithTag("Light");
        darkObjects = GameObject.FindGameObjectsWithTag("Dark");
        light =  GameObject.Find("Light 2D").GetComponent<Light2D>();
        

    }


    public void Update () 
    {
        Switcher();
        Debugs();
    }


    public void Inverse () {
        inverse = !inverse;
        StartCoroutine("SmoothChangeLightIntensivity");
    }

    public void Switcher () 
    {
        if (isLight) 
        {
            foreach (GameObject obj in lightObjects)
            {
                obj.SetActive(true);


            }
            foreach (GameObject obj in darkObjects)
            {
                obj.SetActive(false);
            }

        }
        if (!isLight)
        {
            foreach (GameObject obj in lightObjects)
            {
                obj.SetActive(false);


            }
            foreach (GameObject obj in darkObjects)
            {
                obj.SetActive(true);
            }
        }




    }

#if UNITY_EDITOR    


    void Debugs () 
    {
        foreach (GameObject obj in lightObjects)
        {
            Debug.Log(obj);
        }
        foreach (GameObject obj in darkObjects)
        {
            Debug.Log(obj);
        }

    }

#endif




    IEnumerator SmoothChangeLightIntensivity () 
    {
        for (int i = 0; i<=3; i++ )
        {
            if (inverse && light.intensity >=0.1f)
            {
                light.intensity -= (maxLight-minLight)/4;
                isLight = false;

            }
            else if (!inverse && light.intensity <0.99f)
            {
                light.intensity += (maxLight-minLight)/4;
                isLight = true;

            }
            yield return new WaitForSeconds(0.1f);



        }


    }





}
