using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Resume : MonoBehaviour
{
    [SerializeField]Button _resumeButton;
    [SerializeField]TMPro.TMP_InputField _inputField;
    public void UpdateResume(CandidateStats candidateStats)
    {
        
    }
    private void OnEnable() 
    {
        _resumeButton.onClick.AddListener(Disable);
    }
    private void OnDisable() 
    {
        _resumeButton.onClick.RemoveListener(Disable);
    }
    private void Disable()
    {
        gameObject.SetActive(false);
    }

    public void OnTextSelected(string text)
    {
        Debug.Log(_inputField.caretPosition);
        Debug.Log(_inputField.caretWidth);
    }
}
