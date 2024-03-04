using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PadelOne : MonoBehaviour
{
    public static PadelOne _iPadelOne { get; private set; }

    [SerializeField] float _padelSpeed = 5f;
   
    private Vector2 _moveDirection = Vector2.zero;
    private Vector3 _scaleOfPaddle = new (0.2f, 2.5f, 1);

    public InputAction _playerOneControls;
    public GameObject  _paddle;

    private Rigidbody2D _rb;

    private bool  _hasPowerUp;
    private bool  _hasUsedPowerUp;
    private int[] _powerUps = { 1, 2, 3, 4, 5, 6 };
    private int   _currentPowerUp;
    //Start Logic ===================================================
    private void Awake()
    {
        if (_iPadelOne != null && _iPadelOne != this)
        {
            Destroy(this);
        }
        else
        {
            _iPadelOne = this;
        }
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    //Input control==================================================
    private void OnEnable()
    {
        _playerOneControls.Enable();
    }
    private void OnDisable()
    {
        _playerOneControls.Disable();
    }

    private void Update()
    {
        //Movement on paddel y only.----------------------------------
        _moveDirection = _playerOneControls.ReadValue<Vector2>();
        _rb.velocity = new Vector2(_moveDirection.x * _padelSpeed, _moveDirection.y * _padelSpeed);
        //For player one power up -------------------------------------
        if(_hasPowerUp == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PowerUp(_currentPowerUp);
            }
        }
    }


    //Power Up's =====================================================
    //Income Data ----------------------------------------------------
    public void IncomeData()
    {
        _hasPowerUp = true;
        SetUpPowerLogic();
    }
    private void SetUpPowerLogic()
    {
        if(_hasUsedPowerUp == false)
        {
            _currentPowerUp = _powerUps[Random.Range(0, _powerUps.Length)];
            _hasUsedPowerUp = true;
            //Set Animator to right power up
        }
    }
    //Speed of paddle ------------------------------------------------
    private void PowerUp(int whichPowerUp)
    {
            switch (whichPowerUp)
            {
                case 1:
                    // ball faster
                    BallMovement._instance.BallPowerUp(true);
                    break;
                case 2:
                    // ball slow down
                    BallMovement._instance.BallPowerUp(false);
                    break;
                case 3:
                    //paddle bigger
                    _paddle.transform.localScale = new Vector3(0.2f, 3f, 1f);
                    break;
                case 4:
                    //paddle smaller
                    _paddle.transform.localScale = new Vector3(0.2f, 2f, 1f);
                    break;
                case 5:
                    //paddle slower
                    _padelSpeed = 7f;
                    break;
                case 6:
                    //paddle faster
                    _padelSpeed = 25f;
                    break;
            }
        StartCoroutine(PowerUpTimer(4));
    }
    //Restore state of paddle --------------------------------------
    private IEnumerator PowerUpTimer(int _timer)
    {
        _hasPowerUp = false;
        int i = 0;
        while (i < _timer)
        {
            if (i == 3)
            {
                _padelSpeed = 15f;
                _paddle.transform.localScale = _scaleOfPaddle;
                //change ainmator on power manager to show power up.
                _hasUsedPowerUp = false;
            }
            i++;
            yield return new WaitForSeconds(1);
        }
    }
}   
