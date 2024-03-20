using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalWinnerFont : MonoBehaviour
{
    public static GoalWinnerFont _goalWinnerTextIntance { get; private set; }
    public GameObject _goalWinnerTextObject;
    public TextMeshPro _goalFontText;

    private float _xPostionForText = 5;


    private void Awake()
    {
        if (_goalWinnerTextIntance != null && _goalWinnerTextIntance != this)
        {
            Destroy(this);
        }
        else
        {
            _goalWinnerTextIntance = this;
        }
        _goalWinnerTextObject.transform.localScale = new Vector3(0, 0, 0);
        _goalFontText = GetComponentInChildren<TextMeshPro>();   
    }
    public void PlayerGoalLeft()
    {
        _goalFontText.text = "Goal!!!";
        float _zRotation = Random.Range(-10f, 10f);
        _goalWinnerTextObject.transform.position = new Vector3(_xPostionForText, 0, 0);
        _goalWinnerTextObject.transform.Rotate(0, 0, _zRotation);
        _goalWinnerTextObject.transform.localScale = new Vector3(1, 1, 0);
        StartCoroutine(PlayerScored(3,_goalWinnerTextObject));
        CrowdAnimationController._crowdAnimationInstacen.CrowdCheeringGoAnimaitonin(5);
    }
    public void PlayerGoalRight()
    {
        _goalFontText.text = "Goal!!!";
        float _zRotation = Random.Range(-10f, 10f);
        _goalWinnerTextObject.transform.position = new Vector3(-_xPostionForText, 0, 0);
        _goalWinnerTextObject.transform.Rotate(0, 0, _zRotation);
        _goalWinnerTextObject.transform.localScale = new Vector3(1, 1, 0);
        StartCoroutine(PlayerScored(3, _goalWinnerTextObject));
        CrowdAnimationController._crowdAnimationInstacen.CrowdCheeringGoAnimaitonin(5);
    }

    public void PlayerWinner()
    {
        _goalFontText.text = "Winner!!!";
        _goalWinnerTextObject.transform.position = new Vector3(0, 0, 0);
        _goalWinnerTextObject.transform.Rotate(0, 0, 0);
        _goalWinnerTextObject.transform.localScale = new Vector3(1, 1, 0);
        CrowdAnimationController._crowdAnimationInstacen.CrowdCheeringGoAnimaitonin(10);
    }

    private IEnumerator PlayerScored(int _timer, GameObject _goal)
    {
        int i = 0;
        while (i < _timer)
        {
            if(i == 2)
            {
                _goal.transform.localScale = new Vector3(0, 0, 0);
                _goalWinnerTextObject.transform.Rotate(0, 0, 0);
            }
            i++;
            yield return new WaitForSeconds(1);
        }
    }

}
