using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroAnimationManager : MonoBehaviour
{
    public GameObject _plusFiveArrmor;
    public GameObject _introBoradLights;
    public GameObject _pongText;
    public GameObject _AgainText;
    public GameObject _SlideFadeImgine;
    public GameObject _whiteSquareFade;
    public GameObject _1player;
    public GameObject _2player;
    public GameObject _message;
    public GameObject _serectSound;
    public GameObject _gameSelector;
    public GameObject _vCamera1Main;
    public GameObject _vCamera2Zoomed;
    public GameObject _easyAI;
    public GameObject _mediumAI;
    public GameObject _hardAI;
    public GameObject _back;
    public GameObject _showControls;

    private void Start()
    {
        _plusFiveArrmor.SetActive(false);
        _introBoradLights.SetActive(false);
        _pongText.SetActive(false);
        _AgainText.SetActive(false);
        _SlideFadeImgine.SetActive(false);
        _whiteSquareFade.SetActive(false);
        _1player.SetActive(false);
        _2player.SetActive(false);
        _message.SetActive(false);
        _serectSound.SetActive(false);
        _gameSelector.SetActive(false);
        _vCamera1Main.SetActive(true);
        _vCamera2Zoomed.SetActive(false);
        _easyAI.SetActive(false);
        _mediumAI.SetActive(false);
        _hardAI.SetActive(false);
        _back.SetActive(false);
        _showControls.SetActive(false);
        StartCoroutine(OpeningCutScene(11));
    }


    private IEnumerator OpeningCutScene(int _timer)
    {
        int i = 0;
        while (i < _timer)
        {
            if(i == 1)
            {
                _plusFiveArrmor.SetActive(true);
            }
            if (i == 3)
            {
                _SlideFadeImgine.SetActive(true);
                _vCamera2Zoomed.SetActive(true);
                _vCamera1Main.SetActive(false);
            }

            if(i == 4)
            {
                _plusFiveArrmor.SetActive(false);
                _SlideFadeImgine.SetActive(false);
                _introBoradLights.SetActive(true);
                _vCamera2Zoomed.SetActive(false);
                _vCamera1Main.SetActive(true);
                MusicManager._iMusicManager.IntroTrackStart();
            }

            if(i == 5)
            {
                _pongText.SetActive(true);
            }
            if (i == 6)
            {
               
                _AgainText.SetActive(true);
                _whiteSquareFade.SetActive(true);
                IntroBoradAnimControllerSrpict._iIntroBoradAnimControllerSrpict.SwtichAnimStates();
                _1player.SetActive(true);
                _2player.SetActive(true);
                _message.SetActive(true);
                _gameSelector.SetActive(true);
                _easyAI.SetActive(true);
                _mediumAI.SetActive(true);
                _hardAI.SetActive(true);
                _back.SetActive(true);
                _showControls.SetActive(true);
            }
            if (i == 9)
            {
                IntroBoradAnimControllerSrpict._iIntroBoradAnimControllerSrpict.SwtichAnimStates();
                _serectSound.SetActive(true); MusicManager._iMusicManager.MusicStart();
            }


            i++;
            yield return new WaitForSeconds(1);
        }
    }
}
