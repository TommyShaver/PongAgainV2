using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    //Since this hold most of the scene magament I will make a new script that holds more global information for which player or AI is selected.
    public static SceneLoader _isceneLoader { get; set; }
    public GameObject _canvans;
    public int _AILevel;

    //Game waking up logic -----------------------------------------------------------------------
    private void Awake()
    {
        if (_isceneLoader != null && _isceneLoader != this)
        {
                Destroy(this);
        }
        else
        {
                _isceneLoader = this;
        }
       
    }

    private void Start()
    {
        //Loads gobal info scene and goes from there. This scene should never be unloaded do to it holding all information.
        //Please load and unload scenes using SceneAsync.
        //This is just more information for me on github but hello to other that read this.
        //I am sure this is not the proper way to do this but hey we are here. -_-
        SceneManager.LoadSceneAsync("TitleScreen", LoadSceneMode.Additive);
        _canvans.SetActive(true);
    }

    public void AISelection(int i)
    {
        _AILevel = i;
    }


    //Scene loader logic ----------------------------------------------------------------------------
    public void LoadNextScene()
    {
        //This coroutine is what always for animation to load.
        StartCoroutine(SwitchScenes(4));
    }

    private IEnumerator SwitchScenes(int _timer)
    {
        int i = 0;
        while (i < _timer)
        {
            if(i == 0)
            {
                SceneAnimScript._iSceneAnimScript.StartAnimationFadeIn();
                SceneSoundEffects._isceneSoundEffects.StartSliderSFX();
            }
            if(i == 2)
            {
                SceneManager.LoadSceneAsync("GamePlayScene", LoadSceneMode.Additive);
                SceneManager.UnloadSceneAsync("TitleScreen");
            }
            if(i== 3)
            {
                SceneAnimScript._iSceneAnimScript.StartAnimationFadeOut();
                SceneSoundEffects._isceneSoundEffects.StartFadeOutSFX();
            }
            i++;
            yield return new WaitForSeconds(1);
        }
    }
}