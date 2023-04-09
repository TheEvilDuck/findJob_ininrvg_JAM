using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BodyParts
{
    [SerializeField]public CandidatePart[] bodies;
    [SerializeField]public CandidatePart[] heads;
    [SerializeField]public CandidatePart[] eyes;
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
    [SerializeField]Documents _documents;
    [SerializeField]Note _note;
    private Candidate[] _candidats;
    private List<IRequirement>_requirements = new List<IRequirement>();
    private int _selectedRequirementIndex = -1;
    private int _currentCandidat = 0;
    private int _points = 0;
    public UnityEvent candidateChanged = new UnityEvent();
    private string _positionName;
    private bool _askMode;
    public bool candidateHovered;
    private IRequirement _askedRequirement;
    private List<string>_positionNames = new List<string>
    {
        "Повар","Слесарь","Автомеханик","Врач","Эксперт на программу мужское-женское","Пекарь","Велосипедист","Курьер","Водитель",
        "Священник","Смотритель клуба","Администратор","Программист","Unity разработчик","QA инженер","Front-end разработчик","Охранник",
        "Президент","Продавец в секс-шопе","Администратор-кассир"
    };
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
                if (!_askMode)
                {
                    if (_selectedRequirementIndex<0)
                        return;
                    _candidats[_currentCandidat].candidateStats.patience-=1;
                    IRequirement currentRequirement = _requirements[_selectedRequirementIndex];
                    string stringForNote = currentRequirement.GetResumeLine(_candidats[_currentCandidat].candidateStats.GetLiedStats(_requirements));
                    _note.AddNote(stringForNote,_requirements[_selectedRequirementIndex]);
                }else
                {
                    if (_askedRequirement!=null)
                    {
                        if (!_askedRequirement.GenerateDocument(_documents,candidate.candidateStats))
                            Debug.Log("Can't provide document");
                    }
                }
            });
            _candidats[i].candidateHovered.AddListener(OnCandidateHovered);
            _candidats[i].gameObject.SetActive(false);

        }
    }
    private void Start() 
    {
        _positionName = _positionNames[UnityEngine.Random.Range(0,_positionNames.Count)];
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
        PlayerPrefs.SetInt("Points",_points);
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
            _visuals.UpdateResume(_candidats[_currentCandidat].candidateStats.GetLiedStats(_requirements),_positionName);
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
            bool found = false;
            foreach (NoteLine noteLine in _note.noteLines)
            {
                CandidateStats goal = CandidateStats.GetBlankStats();
                goal = noteLine.requirement.GetIdealCandidateStats(goal);
                bool passed = requirement.CompareRequirement(goal);
                if (passed)
                {
                    found = true;
                    if (requirement.CompareRequirement(_candidats[_currentCandidat].candidateStats)&&noteLine.selected)
                        _points+=_pointsPerRequirementMiss;
                    else
                        _points-=_pointsPerRequirementPass;
                    break;
                }
            }
            if (!found)
            {
                _points-=_pointsPerRequirementMiss;
            }
        }
        NextCandidate();
    }
    public void CandidateCanceled()
    {
        _points-=_pointsForCalcelingCandidate;
        foreach (IRequirement requirement in _requirements)
        {
            bool found = false;
            foreach (NoteLine noteLine in _note.noteLines)
            {
                CandidateStats goal = CandidateStats.GetBlankStats();
                goal = noteLine.requirement.GetIdealCandidateStats(goal);
                bool passed = requirement.CompareRequirement(goal);
                if (passed)
                {
                    found = true;
                    if (requirement.CompareRequirement(_candidats[_currentCandidat].candidateStats)&&noteLine.selected)
                        _points-=_pointsPerRequirementPass;
                    else
                        _points+=_pointsPerRequirementMiss;
                    break;
                }
            }
            if (!found)
            {
                _points-=_pointsPerRequirementMiss;
            }
        }
        NextCandidate();
    }
    public void OnAskMode(IRequirement requirement)
    {
        _askMode = true;
        _askedRequirement = requirement;
    }
    public void ExitAskMode()
    {
        _askMode =false;
        _askedRequirement = null;
    }
    public void OnCandidateHovered(bool hovered)
    {
        candidateHovered = hovered;
    }
    public void OnPlayerClicked(int mouseButton)
    {
        if (_askMode&&!candidateHovered)
        {
            ExitAskMode();
        }
    }
}
