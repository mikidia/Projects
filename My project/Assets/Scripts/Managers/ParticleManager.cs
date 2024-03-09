using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

	#region Declaration 

	[SerializeField] ParticleSystem _bloodParticleSystem;

	#endregion
	public void BloodParticles () 
	{
        _bloodParticleSystem.Play();
    }



}
