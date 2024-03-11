using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

	#region Declaration 

	[SerializeField] ParticleSystem _bloodParticleSystem;
	public static ParticleManager instance;

    #endregion

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
    }

    public void BloodParticles () 
	{
        _bloodParticleSystem.Play();
    }



}
