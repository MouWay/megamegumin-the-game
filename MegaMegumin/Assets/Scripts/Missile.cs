using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private GameObject _explotion;

    private float _speed;
    private float _direction;
    private List<string> _explosionTags;

    private void Start()
    {
        _explosionTags = new List<string> {"Enemy", "MissileDestroyer", "Boss", "Ground"};
        _direction = transform.rotation.z == 1 ? -1 : 1;
        _speed = 10f;    
    }

    private void Update()
    {
        transform.position += _direction * _speed * Time.deltaTime * Vector3.left;
    }

    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (CheckTag(_collision)) 
        {
            Explode();
        }
    }

    private void Explode()
    {
        Instantiate(_explotion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private bool CheckTag(Collision2D collision)
    {
        foreach (string tag in _explosionTags)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }
}
