using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager _iMusicManager { get; set; }

    public AudioSource _mainIntroSong;
    public AudioSource _pickUpBar;
    public AudioSource _selectorTheme;

    private float _minVolume = 0f;
    private float _maxVolume = 1f;
    private float _fadeInDuration = .1f;
    private float _fadeOutDuration = .5f;

    private IEnumerator _fadeOut;
    private IEnumerator _fadeIn;

    private void Awake()
    {
        if (_iMusicManager != null && _iMusicManager != this)
        {
            Destroy(this);
        }
        else
        {
            _iMusicManager = this;
        }
    }

    // Start of game music-------------------------------------------------
    public void IntroTrackStart()
    {
        _mainIntroSong.Play();
    }

    public void MusicStart()
    {
        StartCoroutine(MusicStartPlaying(5));
    }

    private IEnumerator MusicStartPlaying(int _timer)
    {
        int i = 0;
        while (i < _timer)
        {
            if (i == 0)
            {
                _pickUpBar.Play();
            }
            if (i == 1)
            {
                _selectorTheme.Play();
            }
            i++;
            yield return new WaitForSeconds(1);
        }
    }

    // Music fade ----------------------------------------------------------
    public void StartTheMusicFadeOut()
    {
        _fadeOut = FadeOut(_selectorTheme, _fadeOutDuration, _minVolume);
        StartCoroutine(_fadeOut);
    }

    public void StartTheMusicFadeIn()
    {
        StopAllCoroutines();
        _fadeIn= FadeIn(_selectorTheme, _fadeInDuration, _maxVolume);
        StartCoroutine(_fadeIn);
    }


    private IEnumerator FadeOut(AudioSource _volume, float _durination, float _tragetVolume)
    {
        float _timer = 0f;
        float _currentVolume = _volume.volume = 1f;
        float _targetValue = Mathf.Clamp(_tragetVolume, _minVolume, _maxVolume);

        while (_volume.volume > 0)
        {
            _timer += Time.deltaTime;
            var _newVolume = Mathf.Lerp(_currentVolume, _targetValue, _timer / _durination);
            _volume.volume = _newVolume;
            yield return null;
        }
    }
    // ----------------------------------------------------------------------------------------------
    IEnumerator FadeIn(AudioSource _volume, float _durination, float _tragetVolume)
    {
        float _timer = 0f;
        float _currentVolume = _volume.volume = 0;
        float _targetValue = Mathf.Clamp(_tragetVolume, _minVolume, _maxVolume);

        while (_timer < _durination)
        {
            _timer += Time.deltaTime;
            var _newVolume = Mathf.Lerp(_currentVolume, _targetValue, _timer / _durination);
            _volume.volume = _newVolume;
            yield return null;
        }
    }

}
