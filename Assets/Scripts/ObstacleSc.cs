using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSc : MonoBehaviour
{
    public float _destroyPosition;

    GameControllerSc _gameControllerSC;
    [HideInInspector]
    public float _timeCounter;
    [HideInInspector]
    public AudioSource sound;

    void Start()
    {
        _gameControllerSC = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerSc>();
    }
    
    void Update()
    {
        if (_gameControllerSC.gameStart)
        {
            if(_timeCounter <= 12f)
            {
                _timeCounter = _gameControllerSC._kmValueInt / 30f;
            }
            this.transform.Translate(Vector3.left * Time.deltaTime * (10f + _timeCounter));
            if (this.transform.position.x <= _destroyPosition)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void PlaySoundOfItem()
    {

        sound.Play();

    }
}
