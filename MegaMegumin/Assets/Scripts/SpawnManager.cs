using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _defuserPrefab;
    [SerializeField] private GameObject _dinoPrefab;
    [SerializeField] private List<Transform> _defuserSpawnPoints;
    [SerializeField] private List<Transform> _dinoSpawnPoints;

    private int _difficulty;
    private List<int> _spawnCount;
    private List<int> _spawnIndexes;

    private void Start()
    {
        _spawnIndexes = new List<int>();
        _spawnCount = new List<int> { 30, 50, 100 };
        _difficulty = PlayerPrefs.GetInt("Difficulty");
        Instantiate(_defuserPrefab, _defuserSpawnPoints[(int)Random.Range(0, 3)]);
        if (_difficulty == 2)
        {
            foreach (Transform sp in _dinoSpawnPoints)
            {
                Instantiate(_dinoPrefab, sp);
            }
        } else
        {
            int _currentIndex = (int)Random.Range(0, 99);
            Debug.Log(_currentIndex);
            SpawnEnemy(_currentIndex);
            while (_spawnIndexes.Count != _spawnCount[_difficulty])
            {
                _currentIndex = (int)Random.Range(0, 99);
                if (_spawnIndexes.Contains(_currentIndex) == false)
                {
                    SpawnEnemy(_currentIndex);
                }
            }
        }
    }

    private void SpawnEnemy(int currentIndex)
    {
        _spawnIndexes.Add(currentIndex);
        Instantiate(_dinoPrefab, _dinoSpawnPoints[currentIndex]);
    }
}
