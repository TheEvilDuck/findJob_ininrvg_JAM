using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BodyParts
{
    [SerializeField]public CandidatePart[] bodies;
    [SerializeField]public CandidatePart[] heads;
}
[RequireComponent(typeof(SceneLoader))]
public class Game : MonoBehaviour
{
    [SerializeField]int _pointsForCalcelingCandidate = 10;
    [SerializeField]int _pointsPerRequirementMiss = 4;
    [SerializeField]int _pointsPerRequirementPass = 6;
    [SerializeField]int _candidatsCount = 3;
    [SerializeField]BodyParts _bodyParts;
    [SerializeField]Visuals _visuals;
    private Candidate[] _candidats;
    private List<IRequirement>_requirements = new List<IRequirement>();
    private int _selectedRequirementIndex = -1;
    private int _currentCandidat = 0;
    private int _points = 0;
    public UnityEvent candidateChanged = new UnityEvent();
    private IRequirement _noteRequirement;
    private void GenerateCandidats(int count)
    {
        _candidats = new Candidate[count];
        for (int i = 0;i<count;i++)
        {
            GameObject obj = new GameObject();
            _candidats[i] = obj.AddComponent<Candidate>();
            _candidats[i].GenerateStats();
            _candidats[i].GenerateVisuals(_bodyParts, obj.transform);
            _candidats[i].candidateClicked.AddListener((Candidate candidate)=>
            {
                if (_selectedRequirementIndex<0)
                    return;
                bool requirementPassed = _requirements[_selectedRequirementIndex].CompareRequirement(candidate.candidateStats.GetLiedStats(_requirements));
                Debug.Log($"{_requirements[_selectedRequirementIndex].ConvertToString()}: {requirementPassed}");
            });
            _candidats[i].gameObject.SetActive(false);

        }
    }
    private void Start() 
    {
        _requirements = RequirementGenerator.GenerateRequirements();
        GenerateCandidats(_candidatsCount);
        for (int i = 0;i<_requirements.Count;i++)
        {
            _visuals.CreateSelector(_requirements[i],i).AddListener((bool value,int index)=>
            {
                if (value)
                _selectedRequirementIndex = index;
                else if(_selectedRequirementIndex==index)
                    _selectedRequirementIndex = -1;
            });
        }
        CurrentCandidateInit();
    }
    private void GameOver()
    {
        GetComponent<SceneLoader>().LoadScene();
    }
    private void CurrentCandidateInit()
    {
        candidateChanged?.Invoke();
        _candidats[_currentCandidat].gameObject.SetActive(true);
        _visuals.UpdatePointsText(_points);
        bool candidatProvidedResume = _candidats[_currentCandidat].candidateStats.WillProvideAResume();
        _visuals.SetResumeVisibility(candidatProvidedResume);
        if (candidatProvidedResume)
        {
            _visuals.UpdateResume(_candidats[_currentCandidat].candidateStats.GetLiedStats(_requirements));
        }
    }
    private void NextCandidate()
    {
        _candidats[_currentCandidat].RemoveCandidate();
        _currentCandidat++;
        if (_currentCandidat>=_candidatsCount)
        {
            GameOver();
            return;
        }
        CurrentCandidateInit();
    }
    public void CandidateApproved()
    {
        foreach (IRequirement requirement in _requirements)
        {
            bool requirementPassed = requirement.CompareRequirement(_candidats[_currentCandidat].candidateStats);
            if (requirementPassed)
                _points+=_pointsPerRequirementPass;
            else
                _points-=_pointsPerRequirementMiss;
        }
        NextCandidate();
    }
    public void CandidateCanceled()
    {
        _points-=_pointsForCalcelingCandidate;
        foreach (IRequirement requirement in _requirements)
        {
            bool requirementPassed = requirement.CompareRequirement(_candidats[_currentCandidat].candidateStats);
            if (requirementPassed)
                _points-=_pointsPerRequirementPass;
            else
                _points+=_pointsPerRequirementMiss;
        }
        NextCandidate();
    }
    public void OnNoteSelected(bool selected,IRequirement requirement)
    {
        if (selected)
            _noteRequirement = requirement;
        else
            _noteRequirement = null;
    }
    public void OnCandidateAskedAbout(IRequirement requirement)
    {

    }
}
