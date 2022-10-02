using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private float _speed;
    private float _direction;

    private void Start()
    {
        _direction = transform.rotation.z == 1 ? -1 : 1;
        _speed = 10f;    
    }

    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime * Vector3.left;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MissileDestroyer"))
        {
            Destroy(this.gameObject);
        }
    }
}
