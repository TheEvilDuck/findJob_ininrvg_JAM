using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    [SerializeField]Button _noteButton;
    [SerializeField]Transform _content;
    [SerializeField]UISelector _notePrefab;
    public void Disable()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable() {
        _noteButton.onClick.AddListener(Disable);
    }
    private void OnDisable() {
        _noteButton.onClick.RemoveListener(Disable);
    }
    public void AddNote(string text)
    {
        UISelector button = Instantiate(_notePrefab,_content);
        button.ConnectVisuals(text,0);
    }
}
