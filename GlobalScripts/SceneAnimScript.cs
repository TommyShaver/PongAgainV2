using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAnimScript : MonoBehaviour
{
    //This is the script where I store all my animation for scene changes inbetween scenes;
    //This give more opptions that I can work with which is what I want more things to play with;


    public static SceneAnimScript _iSceneAnimScript { get; set; }
    private Animator _animator;

    //Game waking up logic -----------------------------------------------------------------------
    private void Awake()
    {
        if (_iSceneAnimScript != null && _iSceneAnimScript != this)
        {
            Destroy(this);
        }
        else
        {
            _iSceneAnimScript = this;
        }
        _animator = GetComponent<Animator>();
        _animator.SetBool("default", true);
    }

    public void StartAnimationFadeIn()
    {
        _animator.SetBool("fade_In", true);
        _animator.SetBool("default", false);
    }

    public void StartAnimationFadeOut()
    {
        _animator.SetBool("fade_In", false);
    }


}
