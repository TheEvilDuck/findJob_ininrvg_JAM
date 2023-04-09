using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreLoad : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI _text;

    private void Awake() {
        _text.text = "Очков набрано: "+PlayerPrefs.GetInt("Points",0).ToString();
    }
}
