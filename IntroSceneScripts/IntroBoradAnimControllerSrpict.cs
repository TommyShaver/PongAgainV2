using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBoradAnimControllerSrpict : MonoBehaviour
{
    public static IntroBoradAnimControllerSrpict _iIntroBoradAnimControllerSrpict { get; private set; }
    private Animator _animator;
    private bool _isFlashingAnim;

    private void Awake()
    {
        if (_iIntroBoradAnimControllerSrpict != null && _iIntroBoradAnimControllerSrpict != this)
        {
            Destroy(this);
        }
        else
        {
            _iIntroBoradAnimControllerSrpict = this;
        }
        _animator = GetComponent<Animator>();
    }
   
   
    public void SwtichAnimStates()
    {
        if(!_isFlashingAnim)
        {
            _animator.SetBool("isFlashing", true);
            _isFlashingAnim = true;
        }
       else
        {
            _animator.SetBool("isFlashing", false);
            _animator.SetBool("slowFlashing", true);
        }
    }
  
}
