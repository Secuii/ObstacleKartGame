using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject _kartDestroyedParticle;
    GameControllerSc _gameControlerSc;
    ObstacleSc itemsSC;

    private void Start()
    {
        //find the game controller
        _gameControlerSc = GameObject.FindGameObjectWithTag("GameController").gameObject.GetComponent<GameControllerSc>();
    }

    //collision with anything at the game
    private void OnTriggerEnter(Collider other)
    {
        //if the collision is with the obstacle
        // 1# the game pause
        // 2# instante the destroy particle
        // 3# destroy the player

        if(other.transform.tag == "Obstacle")
        {
            _gameControlerSc.gameStart = false;
            Instantiate(_kartDestroyedParticle, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        //if the collision is with the coins
        // 1# find the script component
        // 2# change the coin value
        // 3# play the coin sound
        // 4# destroy the coin
        if(other.transform.tag == "coins")
        {
            itemsSC = other.transform.parent.gameObject.GetComponent<ObstacleSc>();
            _gameControlerSc.ChangeCoinsValueText();
            itemsSC.PlaySoundOfItem();
            Destroy(other.transform.gameObject);
        }

        //if the collision is with the orbs
        // 1# find the script component
        // 2# change the orb bar value
        // 3# play the orb sound
        // 4# destroy the orb
        if(other.transform.tag == "Orbs")
        {
            itemsSC = other.transform.parent.gameObject.GetComponent<ObstacleSc>();
            _gameControlerSc.ChangeChargedBar(other.transform.name);
            itemsSC.PlaySoundOfItem();
            Destroy(other.transform.gameObject);
        }
    }
}
