using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _defuserPrefab;
    [SerializeField] private GameObject _dinoPrefab;
    [SerializeField] private List<Transform> _spawnPoints;

    private void Start()
    {
        Instantiate(_defuserPrefab, _spawnPoints[(int)Random.Range(0, 3)]);
    }
}
