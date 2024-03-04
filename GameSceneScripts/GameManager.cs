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
        UpdateScore(0, 0);
        _scoreTextGameObject.SetActive(false);
        _GoalTextGameObject.SetActive(false);
        Cursor.visible = false;
    }

    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.P))
        {
            PauseTheGame();
        }
    }

    //Score Power Up Manager ===================================================
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
            if (leftGoal == true)
            {
                GoalWinnerFont._goalWinnerTextIntance.PlayerGoalLeft();
                PadelOne._iPadelOne.IncomeData();
            }
            else
            {
                GoalWinnerFont._goalWinnerTextIntance.PlayerGoalRight();
            }
        }
        else
        {
            GoalWinnerFont._goalWinnerTextIntance.PlayerWinner();
        }
    }


    //Pause Screen Menu ---------------------------------------------
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
}
