using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

public class PadelTwo : MonoBehaviour
{
    public static PadelTwo _iPadelTwo { get; private set; }
    [SerializeField] float _padelSpeed = 5f;
    [SerializeField] int   _aiSpeed;
    private Vector2 _forwardDirection;
    private Vector2 _moveDirection = Vector2.zero;
    private Vector3 _scaleOfPaddle = new(0.2f, 2.5f, 1);

    private Rigidbody2D _rb;
    
 

    public InputAction _playerTwoControls;
    public GameObject  _paddle;

    private bool  _playerTwoIsAi;
    private bool  _hasPowerUp;
    private bool  _hasUsedPowerUp;
    private int[] _powerUps = { 0, 1, 2, 3, 4, 5 };
    private int   _currentPowerUp;
    private int   _aiSelectionSpeed;

    //Start Logic ===================================================
    private void Awake()
    {
        if (_iPadelTwo != null && _iPadelTwo != this)
        {
            Destroy(this);
        }
        else
        {
            _iPadelTwo = this;
        }
    }


    private void Start()
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
            PlayerTwoIsAi(_aiSelectionSpeed);

        }
    }

    // Player Movement and power up control ==================================
    private void PlayerTwoIsHuman()
    {
        _moveDirection = _playerTwoControls.ReadValue<Vector2>();
        _rb.velocity = new Vector2(_moveDirection.x * _padelSpeed, _moveDirection.y * _padelSpeed);
        if (_hasPowerUp == true)
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                PowerUp(_currentPowerUp);
            }
        }
    }

    //AI ==============================================================
    //Incoming AI Data------------------------------------------------
    //Incoming data on if there is an AI this is being called from the scene loader
    public void AIIncomingInfo(int i)
    {
        _aiSelectionSpeed = i;
        _playerTwoIsAi = true;
    }

    //Set diffucutly---------------------------------------------------
    private void PlayerTwoIsAi(int _aiDiffuclty)
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
        SetUpPowerLogic();
    }

    private void SetUpPowerLogic()
    {
        if (_hasUsedPowerUp == false)
        {
            _currentPowerUp = _powerUps[Random.Range(0, _powerUps.Length)];
            _hasUsedPowerUp = true;
            PowerUpManager._iPowerUpManager.PowerUp(_currentPowerUp, false);
        }
    }
    //Speed of paddle ------------------------------------------------
    private void PowerUp(int whichPowerUp)
    {
        switch (whichPowerUp)
        {
            case 0:
                // ball faster
                BallMovement._instance.BallPowerUp(true);
                break;
            case 1:
                // ball slow down
                BallMovement._instance.BallPowerUp(false);
                break;
            case 2:
                //padel bigger
                _paddle.transform.localScale = new Vector3(0.2f, 3f, 1f);
                break;
            case 3:
                //padel smaller player 2 needs fixing.
                PadelOne._iPadelOne.PowerUpEffectsPadelSize();
                break;
            case 4:
                //padel slower player 1 need fixing.
                PadelOne._iPadelOne.PowerUpEffectPadelSpeed();
                break;
            case 5:
                //padel faster 
                _padelSpeed = 25f;
                break;
        }
        StartCoroutine(PowerUpTimer(4));
    }

    public void PowerUpEffectsPadelSize()
    {
        _paddle.transform.localScale = new Vector3(0.2f, 2f, 1f);
        StartCoroutine(PowerUpTimer(4));
    }
    public void PowerUpEffectPadelSpeed()
    {
        _padelSpeed = 7f;
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
                _hasUsedPowerUp = false;
            }
            i++;
            yield return new WaitForSeconds(1);
        }
    }
}
