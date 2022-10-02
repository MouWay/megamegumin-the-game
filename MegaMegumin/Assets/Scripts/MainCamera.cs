using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private float _offset;

    private void Start()
    {
        _offset = 0.4f;
    }
    void Update()
    {
        transform.position = new Vector3(_player.position.x, _player.position.y + _offset, -10);
    }
}
