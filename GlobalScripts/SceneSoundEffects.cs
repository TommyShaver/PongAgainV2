using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSoundEffects : MonoBehaviour
{
    public static SceneSoundEffects _isceneSoundEffects { get; set; }
    public AudioSource _fadeSoundEffect;
    public AudioClip _SceneFadeIn;
    public AudioClip _SceneFadeOut;

    private void Awake()
    {
        if (_isceneSoundEffects != null && _isceneSoundEffects != this)
        {
            Destroy(this);
        }
        else
        {
            _isceneSoundEffects = this;
        }
    }

    public void StartSliderSFX()
    {
        _fadeSoundEffect.clip = _SceneFadeIn;
        _fadeSoundEffect.Play();
    }

    public void StartFadeOutSFX()
    {
        _fadeSoundEffect.clip = _SceneFadeOut;
        _fadeSoundEffect.Play();
    }
}
