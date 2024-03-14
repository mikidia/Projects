using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource src;
    [SerializeField] private AudioClip deathSound, sfx2;
    public static SoundManager instance;


    public void Awake ()
    {
         src = GetComponent<AudioSource>();
        if (instance == null)
        {

            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void DeathSound () 
    {
        src.clip = deathSound;
        src.Play ();





    }




}
