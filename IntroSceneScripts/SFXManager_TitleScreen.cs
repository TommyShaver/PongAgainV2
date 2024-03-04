using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager_TitleScreen : MonoBehaviour
{
    public static SFXManager_TitleScreen _iSFXManager_TitleScreen { get; set; }
    public AudioClip _ballSelection;
    public AudioClip _gameSelected;
    public AudioClip _switchSelection;
    public AudioSource _audioSourceSFX1;
    public AudioSource _audioSourceSFX2;
    private void Awake()
    {
        if (_iSFXManager_TitleScreen != null && _iSFXManager_TitleScreen != this)
        {
            Destroy(this);
        }
        else
        {
            _iSFXManager_TitleScreen = this;
        }
        
    }
   
    public void BallSectionSFX()
    {
        _audioSourceSFX1.clip = _ballSelection;
        _audioSourceSFX1.Play();
    }

    public void GameSelection()
    {
        _audioSourceSFX1.clip = _gameSelected;
        _audioSourceSFX1.Play();
    }
    public void SwitchBetweenSelection()
    {
        _audioSourceSFX2.clip = _switchSelection;
        _audioSourceSFX2.Play();
    }

}
