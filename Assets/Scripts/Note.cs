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
    [SerializeField]Button _noteButton;
    [SerializeField]Transform _content;
    [SerializeField]UISelector _notePrefab;
    [SerializeField]Game _game;
    
    public List<NoteLine>noteLines = new List<NoteLine>();

    public void Disable()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable() {
        _noteButton.onClick.AddListener(Disable);
        _game.candidateChanged.AddListener(OnCandidateChanged);
    }
    private void OnDisable() {
        _noteButton.onClick.RemoveListener(Disable);
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
}
