using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrowdAnimationController : MonoBehaviour
{
    public static CrowdAnimationController _crowdAnimationInstacen { get; private set; }
    private Animator _animator;
    private void Awake()
    {
        if (_crowdAnimationInstacen != null && _crowdAnimationInstacen != this)
        {
            Destroy(this);
        }
        else
        {
            _crowdAnimationInstacen = this;
        }
        _animator = GetComponent<Animator>();
    }

    public void CrowdCheeringGoAnimaitonin(int _time)
    {
        StartCoroutine(CrowdCheeringGo(_time));
        _animator.SetBool("ScoreCheeringAnim", true);
    }
    private IEnumerator CrowdCheeringGo(int _timer)
    {
        int _crowdCheeringTimer;
        _crowdCheeringTimer = _timer - 1;
        int i = 0;
        while (i < _timer)
        {
            if(i == _crowdCheeringTimer)
            {
                _animator.SetBool("ScoreCheeringAnim", false);
            }
            i++;
            yield return new WaitForSeconds(1);
            Debug.Log("Crowd Cheering Timer Set =" + _crowdCheeringTimer);
        }
    }

}
