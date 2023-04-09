using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour
{
    [SerializeField]Button _noteButton;
    [SerializeField]Transform _content;
    [SerializeField]UISelector _notePrefab;
    [SerializeField]Game _game;
    private List<IRequirement>_requirementsNotes = new List<IRequirement>();
    private List<UISelector>_notes = new List<UISelector>();

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
        UISelector button = Instantiate(_notePrefab,_content);
        _requirementsNotes.Add(requirement);
        button.ConnectVisuals(text,_requirementsNotes.Count-1);
        button.objectSelected.AddListener(OnObjectSelected);
        _notes.Add(button);
    }
    private void ClearNote()
    {
        foreach (UISelector note in _notes)
        {
            note.objectSelected.RemoveAllListeners();
            Destroy(note.gameObject);
        }
        _notes.Clear();
        _requirementsNotes.Clear();
    }
    public void OnObjectSelected(bool selected,int index)
    {
        if (index>=_requirementsNotes.Count)
            return;
        IRequirement requirement = _requirementsNotes[index];
        _game.OnNoteSelected(selected,requirement);
    }
    public void OnCandidateChanged()
    {
        ClearNote();
    }
}
