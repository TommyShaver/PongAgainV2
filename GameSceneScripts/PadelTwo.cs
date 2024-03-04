using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PadelTwo : MonoBehaviour
{
    [SerializeField] float _padelSpeed = 5f;
    [SerializeField] int   _aiSpeed;
    private Vector2 _forwardDirection;
    private Vector2 _moveDirection = Vector2.zero;
    private Vector3 _scaleOfPaddle = new(0.2f, 2.5f, 1);

    private Rigidbody2D _rb;
    
    private bool _playerTwoIsAi;

    public InputAction _playerTwoControls;
    public GameObject _paddle;

    private bool _hasPowerUp;
    private int[] _powerUps = { 1, 2, 3, 4, 5, 6 };
    private int _currentPowerUp;

    //Start Logic ===================================================
    public void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _playerTwoControls.Enable();
    }
    private void OnDisable()
    {
        _playerTwoControls.Disable();
    }

    private void Update()
    {
        if(!_playerTwoIsAi)
        {
            // player 2 is a human.
            PlayerTwoIsHuman();
        }
        else
        {
            // player 2 is an AI.
            PlayerTwoIsAi(0);

        }
    }

    // Player Movement and power up control ==================================
    public void PlayerTwoIsHuman()
    {
        _moveDirection = _playerTwoControls.ReadValue<Vector2>();
        _rb.velocity = new Vector2(_moveDirection.x * _padelSpeed, _moveDirection.y * _padelSpeed);
        if (_hasPowerUp == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PowerUp(_currentPowerUp);
            }
        }
    }

    //AI ===============================================================
    //Set diffucutly---------------------------------------------------
    public void PlayerTwoIsAi(int _aiDiffuclty)
    {
       
        if (_aiDiffuclty == 1)
        {
            _aiSpeed = 3;
        }
        if (_aiDiffuclty == 2)
        {
            _aiSpeed = 6;
        }
        if(_aiDiffuclty == 3)
        {
            _aiSpeed = 10;
        }
        _forwardDirection = Vector2.left;
        float targetYpostion = AiUpdateYpostion();
        transform.position = new Vector3(9.5f, targetYpostion, 0);
    }
    //Update Y postion ------------------------------------------------
    private float AiUpdateYpostion()
    {
        
        float _result = transform.position.y;
        if(BallIncoming())
        {
            _result = Mathf.MoveTowards(transform.position.y, BallMovement._instance.transform.position.y, _aiSpeed * Time.deltaTime);
        }
        return _result;
    }
    private bool BallIncoming()
    {
        float _dotP = Vector2.Dot(BallMovement._instance._rb.velocity, _forwardDirection);
        return _dotP < 0f;
    }


    //Power Up's =====================================================
    //Income Data ----------------------------------------------------
    public void IncomeData()
    {
        _hasPowerUp = true;
        _currentPowerUp = _powerUps[Random.Range(0, _powerUps.Length)];

    }
    //Speed of paddle ------------------------------------------------
    public void PowerUp(int whichPowerUp)
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
                _padelSpeed = 7f;
                break;
            case 6:
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
            }
            i++;
            yield return new WaitForSeconds(1);
        }
    }
}
