using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private float _throwForce;
    private Transform _woodcutterTransform;

    private void Start()
    {
        _throwForce = 400f;     
        Rigidbody2D _rigidBody = GetComponent<Rigidbody2D>();
        _woodcutterTransform = transform.parent.gameObject.GetComponent<Transform>().Find("Center").gameObject.GetComponent<Transform>();
        Vector3 direction = transform.position - _woodcutterTransform.position;
        _rigidBody.AddForce(direction * _throwForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
