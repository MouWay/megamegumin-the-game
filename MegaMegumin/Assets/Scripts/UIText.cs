using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _enemiesLeftText;
    [SerializeField] private GameObject _gameOverText;
    [SerializeField] private GameObject _restartButton;
    private int _enemiesCount;
    private bool _isPlayerAlive;
    private bool _isDefuserAlive;

    private void Start()
    {
        _restartButton.GetComponent<Button>().onClick.AddListener(RestartGame);    
    }

    void Update()
    {
        _isDefuserAlive = GameObject.FindGameObjectsWithTag("Boss").Length == 0 ? false : true;
        _isPlayerAlive = GameObject.FindGameObjectsWithTag("Player").Length == 0 ? false : true;
        _enemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        _enemiesLeftText.SetText("Enemies left: {0}", _enemiesCount);
        if (_isPlayerAlive == false)
        {
            _restartButton.SetActive(true);
            _gameOverText.SetActive(true);
        }
        if (_isDefuserAlive == false)
        {
            _gameOverText.GetComponent<TextMeshProUGUI>().text = "You win!";
            _gameOverText.SetActive(true);
            _restartButton.SetActive(true);
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
