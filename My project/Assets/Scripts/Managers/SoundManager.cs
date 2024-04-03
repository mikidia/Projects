using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource src;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip[] backgroundMusic;
    [SerializeField] private AudioClip[] WinMusic;

    public static SoundManager instance;


    public void Awake ()
    {




        src = GetComponent<AudioSource>();




    }

    private void Start ()
    {
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
