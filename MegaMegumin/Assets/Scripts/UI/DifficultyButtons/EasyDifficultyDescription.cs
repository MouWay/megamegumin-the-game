using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class EasyDifficultyDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _descriptionText;
    [SerializeField] private GameObject _blurObject;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _descriptionText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetString("EasyDifficultyDescription");
        _descriptionText.SetActive(true);
        _blurObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _descriptionText.SetActive(false);
        _blurObject.SetActive(false);
    }
}
