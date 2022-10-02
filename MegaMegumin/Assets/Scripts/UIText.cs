using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _enemiesLeftText;
    [SerializeField] private GameObject _gameOverText;
    private int _enemiesCount;
    private bool _isPlayerAlive;

    void Update()
    {
        _isPlayerAlive = GameObject.FindGameObjectsWithTag("Player").Length == 0 ? false : true;
        _enemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        _enemiesLeftText.SetText("Enemies left: {0}", _enemiesCount);
        if (_isPlayerAlive == false)
        {
            _gameOverText.SetActive(true);
        }
    }
}
