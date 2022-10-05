using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _defuserPrefab;
    [SerializeField] private GameObject _dinoPrefab;
    [SerializeField] private GameObject _sky;
    [SerializeField] private GameObject _tiles;
    [SerializeField] private List<Transform> _defuserSpawnPoints;
    [SerializeField] private List<Transform> _dinoSpawnPoints;

    private int _difficulty;
    private List<int> _spawnCount;
    private List<int> _spawnIndexes;
    private List<Color> _colors;

    private void Start()
    {
        _colors = new List<Color> { new Color(1f, 1f, 1f), new Color(0.5f, 1f, 1f), new Color(0.4f, 0f, 1f) };
        SetSceneColor();
        _spawnIndexes = new List<int>();
        _spawnCount = new List<int> { 15, 30, 50 };
        _difficulty = PlayerPrefs.GetInt("Difficulty");
        Instantiate(_defuserPrefab, _defuserSpawnPoints[(int)Random.Range(0, 4)]);
        while (_spawnIndexes.Count != _spawnCount[_difficulty])
            {
                int _currentIndex = (int)Random.Range(0, 99);
                if (_spawnIndexes.Contains(_currentIndex) == false)
                {
                    SpawnEnemy(_currentIndex);
                }
            }
    }

    private void SpawnEnemy(int currentIndex)
    {
        _spawnIndexes.Add(currentIndex);
        Instantiate(_dinoPrefab, _dinoSpawnPoints[currentIndex]);
    }

    private void SetSceneColor()
    {
        int index = (int)Random.Range(0, 3);
        _sky.GetComponent<SpriteRenderer>().color = _colors[index];
        _tiles.GetComponent<Tilemap>().color = _colors[index];
    }
}
