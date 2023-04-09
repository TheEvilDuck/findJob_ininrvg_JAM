using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System.IO;

[System.Serializable]
public class ResumeLines
{
    public string[]lines;
}
public class Resume : MonoBehaviour
{
    [SerializeField]Button _resumeButton;
    [SerializeField]TMP_InputField _inputField;
    [SerializeField]Note _note;
    [SerializeField]Game _game;
    //[SerializeField]TextAsset _beginAgeLines;
    //private ResumeLines resumeLines;
    private Dictionary<string,IRequirement>_resumeRequirements = new Dictionary<string, IRequirement>();

    public void UpdateResume(CandidateStats candidateStats)
    {
        //resumeLines = new ResumeLines();
        //resumeLines = JsonUtility.FromJson<ResumeLines>(_beginAgeLines.text);
        _inputField.text = "                              Резюме\n";
        foreach (IRequirement requirement in candidateStats.requirements)
        {
            string resumeLine = requirement.GetResumeLine(candidateStats);
            _resumeRequirements.TryAdd(resumeLine,requirement);
            _inputField.text+=resumeLine;
        }
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
            foreach(KeyValuePair<string,IRequirement>requirementPair in _resumeRequirements)
            {
                if (requirementPair.Key.Contains(subtext))
                {
                    _note.AddNote(subtext,requirementPair.Value);
                }
            }
        }
    }
}
