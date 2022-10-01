using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    private float _speed;
    private Vector3 _moveVector;
    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _rigidBody = _player.GetComponent<Rigidbody2D>();
        _speed = 10f;
    }

    private void Update()
    {
        float _horizontalMove = _speed * Input.GetAxis("Horizontal") * Time.deltaTime;
        _moveVector = new Vector3(_horizontalMove, 0, 0);
        _player.transform.position += _moveVector;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidBody.AddForce(transform.up * _speed);
        }
    }
}
