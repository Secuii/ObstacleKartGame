using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSc : MonoBehaviour
{

    public ParticleSystem _destroyedKart;
    ParticleSystem.MainModule _gravityModifierKart;


    void Start()
    {
        _gravityModifierKart = _destroyedKart.main;
        StartCoroutine("SlowGravity");
    }
    //Set the particle down the gravity to let it stay at the place when touch the collision
    IEnumerator SlowGravity()
    {
        yield return new WaitForSeconds(1f);

        _gravityModifierKart.gravityModifierMultiplier = 5f;


    }
    

}
