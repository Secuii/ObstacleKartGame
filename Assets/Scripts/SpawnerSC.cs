using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSC : MonoBehaviour
{

    [Header("niveles")]
    public GameObject[] obstacleObj = new GameObject[4];
    public GameObject[] coinObj = new GameObject[12];
    GameControllerSc _gameControlerSc;

    private void Start()
    {
        _gameControlerSc = GameObject.FindGameObjectWithTag("GameController").gameObject.GetComponent<GameControllerSc>();
    }
    //The random will take a value to instantiate a coin or a obstacle
    public void SlowLevelObstacle()
    {
        if (_gameControlerSc.gameStart)
        {
            int randomCoinObs = Random.Range(0, 10);

            if (randomCoinObs <= 7)
            {
                int randomObstacle = Random.Range(0, 4);
                Instantiate(obstacleObj[randomObstacle], this.transform.position, Quaternion.identity);

            }
            else
            {
                int randomCoins = Random.Range(0, 12);
                Instantiate(coinObj[randomCoins], this.transform.position, Quaternion.identity);

            }
        }
    }
}
