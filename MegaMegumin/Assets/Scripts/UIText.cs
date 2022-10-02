using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIText : MonoBehaviour
{
    private TextMeshProUGUI _enemiesLeftText;
    private int _enemiesCount;

    private void Start()
    {
        _enemiesLeftText = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        _enemiesCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        _enemiesLeftText.SetText("Enemies left: {0}", _enemiesCount);
    }
}
