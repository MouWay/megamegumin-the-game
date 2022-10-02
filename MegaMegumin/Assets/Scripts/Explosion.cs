using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float _time = 0f;
    void Update()
    {
        _time += Time.deltaTime;
        if(_time > 0.3f)
        {
            Destroy(this.gameObject);
        }
    }
}
