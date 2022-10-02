using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defuser : MonoBehaviour
{
    private int _enemiesCount;
    private bool _isAlive;
    private float _deathTimer;
    private Animator _animator;


    private void Start()
    {
        _isAlive = true;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _enemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (_isAlive == false)
        {
            _deathTimer += Time.deltaTime;
            if (_deathTimer > 10 && _deathTimer < 12)
            {
                _animator.SetTrigger("Death");
            }  else if (_deathTimer > 12)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Missile") && _enemiesCount == 0)
        {
            GetComponent<AudioSource>().enabled = true;
            _isAlive = false;
        }
    }
}
