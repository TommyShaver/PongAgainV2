using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpUsed : MonoBehaviour
{
    private Vector3 _leftGoalLocation = new Vector3(-12.5f, 4.5f, 0);
    private Vector3 _rightGoalLocation = new Vector3(12.5f, 4.5f, 0);
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && transform.position == _leftGoalLocation)
        {
            Destroy(gameObject);
        }
        if(Input.GetKeyDown(KeyCode.RightShift) && transform.position == _rightGoalLocation)
        {
            Destroy(gameObject);
        }
        
    }
}
