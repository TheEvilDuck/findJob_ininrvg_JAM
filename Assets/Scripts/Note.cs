using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteLine
{
    public IRequirement requirement;
    public UISelector line;
    public bool selected;
}
public class Note : MonoBehaviour
{
    [SerializeField]Transform _content;
    [SerializeField]UISelector _notePrefab;
    [SerializeField]Game _game;
    Vector3 _position;
    bool _isHide = true;
    
    public List<NoteLine>noteLines = new List<NoteLine>();
    private void OnEnable() {
        _game.candidateChanged.AddListener(OnCandidateChanged);
        _position = transform.position;
        HideOrShow();
    }
    private void OnDisable() {
        _game.candidateChanged.RemoveListener(OnCandidateChanged);
    }
    public void AddNote(string text, IRequirement requirement)
    {
        NoteLine noteLine = new NoteLine();
        UISelector button = Instantiate(_notePrefab,_content);
        noteLine.line = button;
        noteLine.requirement = requirement;
        noteLines.Add(noteLine);
        noteLine.line.ConnectVisuals(text,noteLines.Count-1);
        noteLine.line.objectSelected.AddListener(OnObjectSelected);
        noteLine.line.rightClicked.AddListener((int index)=>
        {
            noteLines.Remove(noteLine);
            Destroy(noteLine.line.gameObject);
            for (int i = index;i<noteLines.Count;i++)
            {
                noteLines[i].line.index = i;
            }
        });
    }
    private void ClearNote()
    {
        Debug.Log("Clear note");
        foreach (NoteLine noteLine in noteLines)
        {
            noteLine.line.objectSelected.RemoveAllListeners();
            Destroy(noteLine.line.gameObject);
        }
        noteLines.Clear();
    }
    public void OnObjectSelected(bool selected,int index)
    {
        if (index>=noteLines.Count)
            return;
        noteLines[index].selected = selected;
    }
    public void OnCandidateChanged()
    {
        ClearNote();
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
}
