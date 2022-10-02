using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _defuserPrefab;
    [SerializeField] private GameObject _dinoPrefab;
    [SerializeField] private List<Transform> _defuserSpawnPoints;
    [SerializeField] private List<Transform> _dinoSpawnPoints;

    private void Start()
    {
        Instantiate(_defuserPrefab, _defuserSpawnPoints[(int)Random.Range(0, 3)]);
        foreach (Transform sp in _dinoSpawnPoints){
            Instantiate(_dinoPrefab, sp);
        }
    }
}
