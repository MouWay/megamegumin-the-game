using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defuser : MonoBehaviour
{
    private int _enemiesCount;

    void Update()
    {
        _enemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Missile") && _enemiesCount == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
