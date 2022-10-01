using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private float _speed;
    private float _direction;

    void Start()
    {
        _direction = Mathf.Round(transform.rotation.z);
        Debug.Log(_direction);
        _speed = 10f;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime * Vector3.left;
    }
}
