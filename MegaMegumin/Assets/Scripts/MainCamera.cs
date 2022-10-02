using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;

    void Update()
    {
        transform.position = new Vector3(_player.position.x, -2, -10);
    }
}
