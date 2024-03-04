using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowControls : MonoBehaviour
{
    public GameObject[] _gameObjects;
    
    private void OnMouseEnter()
    {
        _gameObjects[0].SetActive(true);
        _gameObjects[1].SetActive(false);
        _gameObjects[2].SetActive(false);
        _gameObjects[3].SetActive(false);
        if (_gameObjects[4])
        {
            _gameObjects[4].SetActive(false);
        }
        if (_gameObjects[5])
        {
            _gameObjects[5].SetActive(false);
        }
    }
    private void OnMouseExit()
    {
        _gameObjects[0].SetActive(false);
        _gameObjects[1].SetActive(true);
        _gameObjects[2].SetActive(true);
        _gameObjects[3].SetActive(true);
        if (_gameObjects[4])
        {
            _gameObjects[4].SetActive(true);
        }
        if (_gameObjects[5])
        {
            _gameObjects[5].SetActive(true);
        }
    }
}
