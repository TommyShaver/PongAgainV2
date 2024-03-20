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

    private bool _isAI;

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
        //This information is selected by the buttons in the main menu scene
        _AILevel = i;
        _isAI = true;
    }
    

    //Scene loader logic ----------------------------------------------------------------------------
    public void LoadNextScene()
    {
        //This start the animation and loading screen.
        //This is from main menu to game play.
        //I didn't really build this good so now I have to make another function going the other way -_-
        //One day I will learn...
        StartCoroutine(SwitchScenes(4));
    }

    private IEnumerator SwitchScenes(int _timer)
    {
        int i = 0;
        while (i < _timer)
        {
            if(i == 0)
            {
                //Start animation in global info scene.
                SceneAnimScript._iSceneAnimScript.StartAnimationFadeIn();
                SceneSoundEffects._isceneSoundEffects.StartSliderSFX();
            }
            if(i == 2)
            {
                //Start load and unload scenes from game play chck if there is AI.
                SceneManager.LoadSceneAsync("GamePlayScene", LoadSceneMode.Additive);
                SceneManager.UnloadSceneAsync("TitleScreen");
                if(_isAI == true)
                {
                    StartCoroutine(WaitForlevelToLoad());
                    //Started a second timer to give the game time to load the scene.
                    //If this isn't here we get a null reference. 
                }
            }
            if(i == 3)
            {
                //Finish animation in glbal info scene.
                SceneAnimScript._iSceneAnimScript.StartAnimationFadeOut();
                SceneSoundEffects._isceneSoundEffects.StartFadeOutSFX();
            }
            i++;
            yield return new WaitForSeconds(1);
        }
    }

    //This information goes to the Paddel two script that game object is loaded when the scene is called.
    private IEnumerator WaitForlevelToLoad()
    {
        yield return new WaitForSecondsRealtime(2);
        PadelTwo._iPadelTwo.AIIncomingInfo(_AILevel);
    }
}