using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Resume : MonoBehaviour
{
    [SerializeField]Button _resumeButton;
    [SerializeField]TMP_InputField _inputField;
    

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
        int textEnd = _inputField.caretPosition;
        int textEnd2 = _inputField.selectionStringAnchorPosition;
        string subtext = text.Substring(Mathf.Min(textEnd,textEnd2),Mathf.Abs(textEnd-textEnd2));
        if (subtext.Length>0)
        {

        }
    }
}
