using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private GameObject _playButton;
    [SerializeField] private GameObject _exitButton;
    [SerializeField] private GameObject _SetEasyDifficultyButton;
    [SerializeField] private GameObject _SetNormalDifficultyButton;
    [SerializeField] private GameObject _SetHardDifficultyButton;

    void Start()
    {
        PlayerPrefs.SetString("EasyDifficultyDescription", "15 enemies\nSlow speed\nPlayer has 100 HP");
        Time.timeScale = 1;
        _playButton.GetComponent<Button>().onClick.AddListener(ShowSetDifficultyButtons);
        _exitButton.GetComponent<Button>().onClick.AddListener(Quit);
        _SetEasyDifficultyButton.GetComponent<Button>().onClick.AddListener(SetEasyDifficulty);
        _SetNormalDifficultyButton.GetComponent<Button>().onClick.AddListener(SetNormalDifficulty);
        _SetHardDifficultyButton.GetComponent<Button>().onClick.AddListener(SetHardDifficulty);
    }

    private void Play()
    {
        SceneManager.LoadScene(1);
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void SetEasyDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", 0);
        Play();
    }

    private void SetNormalDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", 1);
        Play();
    }

    private void SetHardDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", 2);
        Play();
    }

    private void ShowSetDifficultyButtons()
    {
        _playButton.SetActive(false);
        _exitButton.SetActive(false);
        _SetEasyDifficultyButton.SetActive(true);
        _SetNormalDifficultyButton.SetActive(true);
        _SetHardDifficultyButton.SetActive(true);
    }
}
