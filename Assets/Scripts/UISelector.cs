using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UISelector : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]GameObject _selectedVisuals;
    [SerializeField]TextMeshProUGUI _text;
    [SerializeField]Button _button;
    public int index;
    public UnityEvent<bool,int>objectSelected = new UnityEvent<bool, int>();
    public UnityEvent<int> rightClicked = new UnityEvent<int>();
    public bool selected;
    
    public void ConnectVisuals(string text,int index)
    {
        _text.text = text;
        this.index = index;
        _button.onClick.AddListener(onClick);
    }
    private void onClick()
    {
        selected = !selected;
        _selectedVisuals.SetActive(selected);
        objectSelected?.Invoke(selected,index);
    }
    public void ForceState(bool value)
    {
        selected = value;
        _selectedVisuals.SetActive(selected);
        objectSelected?.Invoke(selected,index);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            rightClicked?.Invoke(index);
        }
    }
}
