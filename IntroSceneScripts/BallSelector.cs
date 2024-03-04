using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallSelector : MonoBehaviour
{
    //This Script is the UI Manager this is all logic for buttons named it horribly.
    //This is a script that take care of two function one to get the mouse location and the selection of
    //UI buttons

    private bool _isPlayer1 = false;
    private bool _aiShown;
    private bool _selectedOnce;
    private int _aiLocation = 1;
    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(-2, -1.15f, 0);
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

   //Basic logic --------------------------------------------------------------------
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            SFXManager_TitleScreen._iSFXManager_TitleScreen.BallSectionSFX();
            if(!_aiShown)
            {
                BallLocationPlayers();
            }
            else
            {
                if(_aiLocation == 0)
                {
                    _aiLocation = 3;
                    BallLocationAI(3);
                }
                else
                {
                    _aiLocation--;
                    BallLocationAI(_aiLocation);
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            SFXManager_TitleScreen._iSFXManager_TitleScreen.BallSectionSFX();
            if (!_aiShown)
            {
                BallLocationPlayers();
            }
            else
            {
                if (_aiLocation == 3)
                {
                    _aiLocation = 0;
                    BallLocationAI(_aiLocation);
                }
                else
                {
                    _aiLocation++;
                    BallLocationAI(_aiLocation);
                }
            }
        }

        if(_selectedOnce == false)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _selectedOnce = true;
                if (!_aiShown)
                {
                    if (!_isPlayer1)
                    {
                        SelectingModes();
                        SFXManager_TitleScreen._iSFXManager_TitleScreen.GameSelection();
                        MusicManager._iMusicManager.StartTheMusicFadeOut();
                    }
                    else
                    {
                        HideTheBall();
                        UIManager._iUIManager.ShowAIOption();
                        AIShown(true);
                        SFXManager_TitleScreen._iSFXManager_TitleScreen.SwitchBetweenSelection();
                    }
                }
                else
                {
                    switch (_aiLocation)
                    {
                        case 0:
                            UIManager._iUIManager.AIEasy();
                            SFXManager_TitleScreen._iSFXManager_TitleScreen.GameSelection();
                            MusicManager._iMusicManager.StartTheMusicFadeOut();
                            break;
                        case 1:
                            UIManager._iUIManager.AIMedium();
                            SFXManager_TitleScreen._iSFXManager_TitleScreen.GameSelection();
                            MusicManager._iMusicManager.StartTheMusicFadeOut();
                            break;
                        case 2:
                            UIManager._iUIManager.AIHard();
                            SFXManager_TitleScreen._iSFXManager_TitleScreen.GameSelection();
                            MusicManager._iMusicManager.StartTheMusicFadeOut();
                            break;
                        case 3:
                            HideTheBall();
                            UIManager._iUIManager.ShowPlayers();
                            AIShown(false);
                            SFXManager_TitleScreen._iSFXManager_TitleScreen.SwitchBetweenSelection();
                            break;

                    }
                }
            }
        }
    }

    public void SelectingModes()
    {
        SceneLoader._isceneLoader.LoadNextScene();
    }


    //Ball location of player selections -----------------------------------
    private void BallLocationPlayers()
    {
       if(_isPlayer1 == true)
        {
            gameObject.transform.position = new Vector3(-2, -1.7f, 0);
            _isPlayer1 = false;
            TextInfoMassager._iTextInfoMassager.PlayerSelectorText(false);
        }
       else
        {
            gameObject.transform.position = new Vector3(-2, -1.15f, 0);
            _isPlayer1 = true;
            TextInfoMassager._iTextInfoMassager.PlayerSelectorText(true);
        }
    }
   
    public void MouseOverText(bool _p1)
    {
        if(!_p1)
        {
            gameObject.transform.position = new Vector3(-2, -1.7f, 0);
            _isPlayer1 = true;
        }
        else
        {
            gameObject.transform.position = new Vector3(-2, -1.15f, 0);
            _isPlayer1 = false;
        }
    }

    //Ball location of AI selections -------------------------------------
    public void BallLocationAI(int i)
    {
        switch (i)
        {
            case 0:
                gameObject.transform.position = new Vector3(-2, -.45f, 0);
                TextInfoMassager._iTextInfoMassager.AISelectorText(0);
                break;
            case 1:
                gameObject.transform.position = new Vector3(-2, -1.15f, 0);
                TextInfoMassager._iTextInfoMassager.AISelectorText(1);
                break;
            case 2:
                gameObject.transform.position = new Vector3(-2, -1.7f, 0);
                TextInfoMassager._iTextInfoMassager.AISelectorText(2);
                break;
            case 3:
                gameObject.transform.position = new Vector3(-2, -2.35f, 0);
                break;

        }
    }

    public void AIShown(bool b)
    {
        _aiShown = b;
    }


    //Small Animation to hide ball when switching states ------------------
    public void HideTheBall()
    {
        StartCoroutine(HideBall(2));
    }
    private IEnumerator HideBall(int _timer)
    {
        int i = 0;
        while (i < _timer)
        {
            if (i == 0)
            {
                _spriteRenderer.enabled = false;
            }
            if (i == 1)
            {
                _spriteRenderer.enabled = true;
                gameObject.transform.position = new Vector3(-2, -1.15f, 0);
                _selectedOnce = false;
            }
            i++;
            yield return new WaitForSeconds(1);
        }
    }
}
