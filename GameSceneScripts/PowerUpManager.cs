using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager _iPowerUpManager { get; private set; }
    public GameObject[] _powerUpPrefabs;
    private Vector3 _spawnPowerUpLeft = new Vector3(-12.5f, 4.5f, 0);
    private Vector3 _spawnPowerUpRight = new Vector3(12.5f, 4.5f, 0);

    private void Awake()
    {
        if (_iPowerUpManager != null && _iPowerUpManager != this)
        {
            Destroy(this);
        }
        else
        {
            _iPowerUpManager = this;
        }
    }

    public void PowerUp(int whichPowerUp, bool p1Goal)
    {
        int prefabIndex = whichPowerUp;


        if(p1Goal == true)
        {
            Instantiate(_powerUpPrefabs[prefabIndex], _spawnPowerUpLeft, Quaternion.identity);
        }
        else
        {
            Instantiate(_powerUpPrefabs[prefabIndex], _spawnPowerUpRight, Quaternion.identity);
        }
        
    }
}
