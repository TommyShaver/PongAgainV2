using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public static BallMovement _instance {get; private set;}

    [SerializeField] float _speedOfBall;
    [SerializeField] float _ballSpeedMovementIncrease = 0;
    [SerializeField] private bool _towardsPlayer1;

    public Rigidbody2D _rb;
    public GameObject  _ball;

    private int _ballLocationX;
    private int _ballLocationY;
   
    // Opening Logic ============================================================
    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
        _rb = GetComponent<Rigidbody2D>();
    }



    //Ball Logic ================================================================
    public void LaunchBall()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        _rb.velocity = new Vector2 (_speedOfBall * x,_speedOfBall * y);
       
        if(x == 1)
        {
            _towardsPlayer1 = false;
        }
        else
        {
            _towardsPlayer1 = true;
        }

        if(y == 1)
        {
            CheckBallLocationNorth(true);
        }
        else
        {
            CheckBallLocationNorth(false);
        }
    }


    // Collision Logic ==========================================================
    //In game collison with walls and padels -----------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Padel"))
        {
            if(_ballSpeedMovementIncrease == 10)
            {
                _ballSpeedMovementIncrease = 0;
                _speedOfBall += 1;
                _rb.velocity = new Vector2(_speedOfBall * _ballLocationX, _speedOfBall * _ballLocationY);
            }
            else
            {
                _ballSpeedMovementIncrease++;
            }
            CheckBallForwardLocationX();  
        }

        if(collision.gameObject.CompareTag("NorthWall"))
        {
            CheckBallLocationNorth(false);
        }
        if(collision.gameObject.CompareTag("SouthWall"))
        {
            CheckBallLocationNorth(true);
        }
    }
    //Collison with goals to show who scored ----------------------------------
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "leftGoal")
        {
            GameManager._gameManagerInstance.UpdateScore(0, 1);
            GameManager._gameManagerInstance.WhichGoalScored(true);
        }
        if (collision.gameObject.tag == "rightGoal")
        {
            GameManager._gameManagerInstance.UpdateScore(1, 0);
            GameManager._gameManagerInstance.WhichGoalScored(false);
        }
    }

    //Check location of ball -------------------------------------------------
    private void CheckBallForwardLocationX()
    {
        if(_towardsPlayer1 == false)
        {
            _ballLocationX = -1;
            _towardsPlayer1 = true;
        }
        else
        {
            _ballLocationX = 1;
            _towardsPlayer1 = false;
        }
    }
    private void CheckBallLocationNorth(bool wasNorthWall)
    {

        if (wasNorthWall == false)
        {
            _ballLocationY = -1;
        }
        else
        {
            _ballLocationY = 1;
        }
        Debug.Log(wasNorthWall);
    }

    

    //Power Ups =================================================================
    public void BallPowerUp(bool speedUp)
    {
        if(speedUp == true)
        {
            _speedOfBall += 5;
            StartCoroutine(BallTimer(4, true));
            _rb.velocity = new Vector2(_speedOfBall * _ballLocationX, _speedOfBall * _ballLocationY);
        }
        else
        {
            _speedOfBall -= 3; 
            StartCoroutine(BallTimer(4, false));
            _rb.velocity = new Vector2(_speedOfBall * _ballLocationX, _speedOfBall * _ballLocationY);
        }
    }
    //Power Up Timer ----------------------------------------------------------
    private IEnumerator BallTimer(int _timer, bool restoreState)
    {
        int i = 0;
        while (i < _timer)
        {
            if (i == 3)
            {
                if(restoreState == true)
                {
                    _speedOfBall -= 5;
                    _rb.velocity = new Vector2(_speedOfBall * _ballLocationX, _speedOfBall * _ballLocationY);
                }
                else
                {
                    _speedOfBall += 3;
                    _rb.velocity = new Vector2(_speedOfBall * _ballLocationX, _speedOfBall * _ballLocationY);
                }
            }
            i++;
            yield return new WaitForSeconds(1);
        }
    }

    //Reset Ball ================================================================
    private void ResetBallPosition()
    {
        _speedOfBall = 5;
        _ball.transform.position = Vector3.zero;

        _rb.velocity = Vector2.zero;
        LaunchBall();
    }
}
