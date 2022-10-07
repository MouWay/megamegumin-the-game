using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private GameObject _explotion;

    private float _speed;
    private float _rotation;
    private List<string> _explosionTags;

    private void Start()
    {
        _explosionTags = new List<string> {"Enemy", "MissileDestroyer", "Boss", "Ground"};
        _rotation = ((transform.eulerAngles.z + 180) % 360) * Mathf.PI / 180;
        _speed = 10f;    
    }

    private void Update()
    {
        Vector3 _direction = new Vector3(Mathf.Cos(_rotation), Mathf.Sin(_rotation), 0);
        transform.localPosition += _speed * Time.deltaTime * _direction;
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
