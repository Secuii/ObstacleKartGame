using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameControllerSc : MonoBehaviour
{

    //Orb variable (canvas images and float value to add to the fill amount
    public Image _barraCargaRoja;
    public Image _barraCargaAzul;
    public Image _barraCargaAmarilla;
    public Image _barraCargaVerde;

    float _orbValue = .2f;

    //Player GO
    public GameObject playerObj;

    //Two variable float and integer (float to use it and integer to print on text)
    [HideInInspector]
    public int _kmValueInt;
    float _kmValueFloat;

    //Coin value to add to the score
    [HideInInspector]
    public int _coinValue = 1;

    //TEXT ( km value and coins value)
    public Text kmValue;
    public Text coinstext;

    //Array player position on street
    [HideInInspector]
    public int[] colocacionPlayer = new int[4] { 0, 0, 1, 2 };

    // variable to save the street 
    Transform _selectedStreet;

    //player poss at the game (1 == red, 2 == blue, 3 == yellow, 4 == green)
    int actualPosPlayer = 4;

    //Bool to set all the game START
    public bool gameStart;

    //Wall obj needed to anim it after start the game
    GameObject _wallObj;
    Animator animWall;

    //Lerp player movement
    float lerpZPlayermovement;
    float lerpYPlayermovement;

    //Player Animator
    Animator playerAnimKart;
    bool _playerActiveAnim;

    //Background music
    public AudioSource gameaudio;

    // Scripts to call
    ButtonSc _buttonSc;
    SpawnerSC _spawnerControllerSc;

    void Start()
    {

        _wallObj = GameObject.FindGameObjectWithTag("WallOBJ");
        playerAnimKart = playerObj.transform.GetComponent<Animator>();
        animWall = _wallObj.transform.GetComponent<Animator>();
        _spawnerControllerSc = GameObject.FindGameObjectWithTag("Respawn").GetComponent<SpawnerSC>();

    }

    void Update()
    {
        //When game start the Km counter will start
        if (gameStart)
        {
            _kmValueFloat = _kmValueFloat + .15f;
            _kmValueInt = Mathf.RoundToInt(_kmValueFloat);
            kmValue.text = _kmValueInt.ToString();
        }

        //The player will lerp his actual position to a new position 
        if (_playerActiveAnim)
        {
            lerpYPlayermovement = lerpYPlayermovement + 0.01f;
            lerpZPlayermovement = lerpZPlayermovement + 0.01f;
            float movimientoYPlayter = Mathf.Lerp(playerObj.transform.position.y, _selectedStreet.transform.position.y, lerpYPlayermovement);
            float movimientoZPlayter = Mathf.Lerp(playerObj.transform.position.z, _selectedStreet.transform.position.z, lerpZPlayermovement);

            playerObj.transform.position = new Vector3(playerObj.transform.position.x, movimientoYPlayter, movimientoZPlayter);

            if (lerpYPlayermovement >= .05f || lerpZPlayermovement >= .05f)
            {
                playerAnimKart.SetInteger("UpDownAnimation", 0);
            }
            if (lerpYPlayermovement >= .25f || lerpZPlayermovement >= .25f)
            {

                lerpYPlayermovement = 0f;
                lerpZPlayermovement = 0f;

                _playerActiveAnim = false;
            }
        }

        //raycast to interact with the world (buttons) (start button)

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.transform.tag == "START")
                {
                    gameaudio.Play();
                    _spawnerControllerSc.InvokeRepeating("SlowLevelObstacle",2f,2f);
                    animWall.SetBool("StartGame", true);
                    gameStart = true;
                    hit.transform.gameObject.SetActive(false);
                }
                if (hit.transform.tag == "Button" && gameStart)
                {
                    if (_playerActiveAnim == false)
                    {
                        _buttonSc = hit.transform.GetComponent<ButtonSc>();
                        _selectedStreet = _buttonSc.pivot.transform;
                        DeteccionPistaEnArray(_buttonSc.pivotValue);
                    }
                }
            }
        }
        //
        //This code is to set the touch for the movil device
        //

        //if (Input.touchCount > 0)
        //{
        //    RaycastHit hit;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        //    if (Physics.Raycast(ray, out hit, 100f))
        //    {
        //        if (hit.transform.tag == "START")
        //        {
        //            gameaudio.Play();
        //            spwn.StartGame();
        //            animWall.SetBool("StartGame", true);
        //            gameStart = true;
        //            hit.transform.gameObject.SetActive(false);
        //        }

        //        if (hit.transform.tag == "Button" && gameStart)
        //        {
        //            if (playerMoveAnim == false)
        //            {
        //                butonSc = hit.transform.GetComponent<ButtonSc>();
        //                pivoteSelected = butonSc.pivot.transform;
        //                DeteccionPistaEnArray(butonSc.pivotValue);

        //            }
        //        }
        //    }
        //}
    }

    // check what street is the car and if the street is in a more value will use up animation or at the other way to down one
    void DeteccionPistaEnArray(int valorArray)
    {
        if (colocacionPlayer[valorArray] == 1)
        {
            if (valorArray < actualPosPlayer)
            {
                playerAnimKart.SetInteger("UpDownAnimation", 1);
            }
            if (valorArray > actualPosPlayer)
            {

                playerAnimKart.SetInteger("UpDownAnimation", -1);
            }

            _playerActiveAnim = true;

            // array to check at what street is the car (this is used to let the car move 1 by 1 the streets and not jump from the green to the red)
            if (valorArray == 0)
            {
                colocacionPlayer = new int[4] { 2, 1, 0, 0 };
                actualPosPlayer = 0;
            }
            if (valorArray == 1)
            {
                colocacionPlayer = new int[4] { 1, 2, 1, 0 };
                actualPosPlayer = 1;
            }
            if (valorArray == 2)
            {
                colocacionPlayer = new int[4] { 0, 1, 2, 1 };
                actualPosPlayer = 2;
            }
            if (valorArray == 3)
            {
                colocacionPlayer = new int[4] { 0, 0, 1, 2 };
                actualPosPlayer = 3;
            }
        }
    }
    // change the coin value every time you touch the coins
    public void ChangeCoinsValueText()
    {
        _coinValue++;
        coinstext.text = _coinValue.ToString();
    }


    //Change the value to the orb bar to use the power ups
    public void ChangeChargedBar(string orbName)
    {
        if (orbName == "RedOrb")
        {
            _barraCargaRoja.fillAmount = _barraCargaRoja.fillAmount + _orbValue;

        }
        if (orbName == "BlueOrb")
        {
            _barraCargaAzul.fillAmount = _barraCargaAzul.fillAmount + _orbValue;
        }
        if (orbName == "YellowOrb")
        {
            _barraCargaAmarilla.fillAmount = _barraCargaAmarilla.fillAmount + _orbValue;
        }
        if (orbName == "GreenOrb")
        {
            _barraCargaVerde.fillAmount = _barraCargaVerde.fillAmount + _orbValue;
        }
    }
}