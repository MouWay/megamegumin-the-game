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
    [SerializeField] private GameObject _mainMenuButton;
    private int _enemiesCount;
    private bool _isPlayerAlive;
    private bool _isDefuserAlive;
    private bool _isGameStopped;

    private void Start()
    {
        Time.timeScale = 1;
        _isGameStopped = false;
        _restartButton.GetComponent<Button>().onClick.AddListener(RestartGame);
        _mainMenuButton.GetComponent<Button>().onClick.AddListener(LoadMainMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isGameStopped)
            {
                HideButtons();
                ResumeGame();
                _isGameStopped = false;
            } else
            {
                _gameOverText.GetComponent<TextMeshProUGUI>().text = "Game paused";
                ShowButtons();
                PauseGame();
                _isGameStopped = true;
            }
        }
        _isDefuserAlive = GameObject.FindGameObjectsWithTag("Boss").Length == 0 ? false : true;
        _isPlayerAlive = GameObject.FindGameObjectsWithTag("Player").Length == 0 ? false : true;
        _enemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        _enemiesLeftText.SetText("Enemies left: {0}", _enemiesCount);
        if (_isPlayerAlive == false)
        {
            ShowButtons();
        }
        if (_isDefuserAlive == false)
        {
            _gameOverText.GetComponent<TextMeshProUGUI>().text = "You won!";
            ShowButtons();
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private void ShowButtons()
    {
        _mainMenuButton.SetActive(true);
        _gameOverText.SetActive(true);
        _restartButton.SetActive(true);
    }
    private void HideButtons()
    {
        _mainMenuButton.SetActive(false);
        _gameOverText.SetActive(false);
        _restartButton.SetActive(false);
    }
}
