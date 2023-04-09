using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class UISelector : MonoBehaviour
{
    [SerializeField]GameObject _selectedVisuals;
    [SerializeField]TextMeshProUGUI _text;
    [SerializeField]Button _button;
    private int _index;
    public UnityEvent<bool,int>objectSelected = new UnityEvent<bool, int>();
    private bool _selected;
    
    public void ConnectVisuals(string text,int index)
    {
        _text.text = text;
        _index = index;
        _button.onClick.AddListener(onClick);
    }
    private void onClick()
    {
        _selected = !_selected;
        _selectedVisuals.SetActive(_selected);
        objectSelected?.Invoke(_selected,_index);
    }
}
