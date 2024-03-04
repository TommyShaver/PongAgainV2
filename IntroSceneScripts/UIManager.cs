using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager _iUIManager { get; set; }
    private Animator _animator;

    // Baisc wake up function ----------------------------
    private void Awake()
    {
        if (_iUIManager != null && _iUIManager != this)
        {
            Destroy(this);
        }
        else
        {
            _iUIManager = this;
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _animator.SetBool("_ShowPlayers", false);
    }

    // UI Animation ---------------------------------------
    public void ShowPlayers()
    {
        _animator.SetBool("_ShowPlayers", false);
    }

    public void ShowAIOption()
    {
        _animator.SetBool("_ShowPlayers", true);
    }

    // Loading AI selection ------------------------------
    public void AIEasy()
    {
        SceneLoader._isceneLoader.AISelection(1);
        SceneLoader._isceneLoader.LoadNextScene();
    }
    public void AIMedium()
    {
        SceneLoader._isceneLoader.AISelection(2);
        SceneLoader._isceneLoader.LoadNextScene();
    }
    public void AIHard()
    {
        SceneLoader._isceneLoader.AISelection(3);
        SceneLoader._isceneLoader.LoadNextScene();
    }
}
