using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _gameManagerInstance {get; private set;}
    public TextMeshProUGUI _scoreText;
    public GameObject _scoreTextGameObject;
    public GameObject _GoalTextGameObject;
    public GameObject _startOfGameCam;
    public GameObject _mainGameCam;
    public GameObject _cmPlayer1Cam;
    public GameObject _cmPlayer2Cam;
    public GameObject _crowdCam1;
    public GameObject _crowdCam2;
    public GameObject _gamePausedScreen;
    public GameObject _returnToTitleScrene;

 
    private int  _playerOneScore = 0;
    private int  _playerTwoScore = 0;
    private bool _gamePaused;
    private bool _isLeftGoal;
    private bool _startOfGame = true;


    //Starting Logic ==================================================================
    private void Awake()
    {
        if (_gameManagerInstance != null && _gameManagerInstance != this)
        {
            Destroy(this);
        }
        else
        {
            _gameManagerInstance = this;
        }

    }

    public void Start()
    {
        //Reset Score
        UpdateScore(0, 0);
        //Set Text up
        _scoreTextGameObject.SetActive(false);
        _GoalTextGameObject.SetActive(false);
        //Hide the Cursor
        Cursor.visible = false;
        //Lanuch the ball
        StartCoroutine(WaitForReset(5));
    }

    private void Update()
    {
       //To load the pause menu
       if(Input.GetKeyDown(KeyCode.P))
        {
            PauseTheGame();
        }
    }

    //Score Power Up Manager ===================================================
    //This information come from the ball movement script ----------------------
    public void UpdateScore(int _scoreToAddPOne,int _scoreToAddPTwo)
    {
            _playerOneScore = _playerOneScore + _scoreToAddPOne;
            _playerTwoScore = _playerTwoScore + _scoreToAddPTwo;
            _scoreText.text = "Score:" + _playerOneScore + "-" + _playerTwoScore;
    }

    public void WhichGoalScored(bool leftGoal)
    {
        if (_playerOneScore != 10 && _playerTwoScore != 10)
        {
            //This is to show text and check to see if a power up is awraded.
            if (leftGoal == true)
            {
                GoalWinnerFont._goalWinnerTextIntance.PlayerGoalLeft();
                _isLeftGoal = true;
                StartCoroutine(WaitForReset(5));
            }
            else
            {
                GoalWinnerFont._goalWinnerTextIntance.PlayerGoalRight();
                _isLeftGoal = false;
                StartCoroutine(WaitForReset(5));
            }
        }
        else
        {
            //And if one of the players hits 10 points game is over.
            GoalWinnerFont._goalWinnerTextIntance.PlayerWinner();
        }
    }


    //Pause Screen Menu ---------------------------------------------
    //We track the pause menu in the game manager so I can keep track of the timing of everything.
    private void PauseTheGame()
    {
        if(_gamePaused == false)
        {
            Time.timeScale = 0;
            _gamePaused = true;
            _gamePausedScreen.SetActive(true);
            _returnToTitleScrene.SetActive(true);
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            _gamePaused = false;
            _gamePausedScreen.SetActive(false);
            _returnToTitleScrene.SetActive(false);
            Cursor.visible = true;
        }
    }

    //Timer ===================================================
    //Ball Reset and start-------------------------------------
    private IEnumerator WaitForReset(int _timer)
    {
        int i = 0;
        while (i < _timer)
        {
            
            if (i == 4)
            {
                BallMovement._instance.ResetBallPosition();
                if (_startOfGame == true)
                {
                    _startOfGame = false;
                }
                else
                {
                    if (!_isLeftGoal)
                    {
                        PadelTwo._iPadelTwo.IncomeData();
                    }
                    else
                    {
                        PadelOne._iPadelOne.IncomeData();
                    }
                }
            }
            i++;
            yield return new WaitForSeconds(1);
        }
    }
}
