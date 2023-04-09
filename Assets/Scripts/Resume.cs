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
    [SerializeField]TMP_InputField _inputField;
    [SerializeField]Note _note;
    [SerializeField]Game _game;
    private bool _isHide = true;
    private Vector3 _position;
    //[SerializeField]TextAsset _beginAgeLines;
    //private ResumeLines resumeLines;
    private Dictionary<string,IRequirement>_resumeRequirements = new Dictionary<string, IRequirement>();

    public void UpdateResume(CandidateStats candidateStats,string positionName)
    {
        //resumeLines = new ResumeLines();
        //resumeLines = JsonUtility.FromJson<ResumeLines>(_beginAgeLines.text);
        _inputField.text = "                              Резюме\n";
        int index = 0;
        int indexToAddPosition = UnityEngine.Random.Range(0,candidateStats.requirements.Count);
        foreach (IRequirement requirement in candidateStats.requirements)
        {
            string resumeLine = requirement.GetResumeLine(candidateStats);
            _resumeRequirements.TryAdd(resumeLine,requirement);
            _inputField.text+=resumeLine;
            if (index==indexToAddPosition)
                _inputField.text+=$"Я очень хочу быть настоящим {positionName}. ";
            index++;

        }
    }
    private void OnEnable() {
        _position = transform.position;
        HideOrShow();
    }
    private void HideOrShow()
    {
        if (_isHide)
            transform.position = new Vector3(1000,1000,1000);
        else
            transform.position = _position;
    }
    public void Enable()
    {
        _isHide = !_isHide;
        HideOrShow();
    }
    public void Enable(bool value)
    {
        _isHide = value;
        HideOrShow();
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
