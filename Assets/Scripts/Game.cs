using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BodyParts
{
    [SerializeField]public CandidatePart[] bodies;
    [SerializeField]public CandidatePart[] heads;
}
public class Game : MonoBehaviour
{
    [SerializeField]BodyParts _bodyParts;
    [SerializeField]Visuals _visuals;
    private Candidate[] _candidats;
    private List<IRequirement>_requirements = new List<IRequirement>();
    private int _selectedRequirementIndex = -1;
    private int _currentCandidat = 0;
    private void GenerateCandidats(int count)
    {
        _candidats = new Candidate[count];
        for (int i = 0;i<count;i++)
        {
            GameObject obj = new GameObject();
            _candidats[i] = obj.AddComponent<Candidate>();
            _candidats[i].GenerateStats(_requirements);
            _candidats[i].GenerateVisuals(_bodyParts, obj.transform);
            _candidats[i].candidateClicked.AddListener((Candidate candidate)=>
            {
                if (_selectedRequirementIndex<0)
                    return;
                bool requirementPassed = _requirements[_selectedRequirementIndex].CompareRequirement(candidate.candidateStats.GetLiedStats());
                Debug.Log($"{_requirements[_selectedRequirementIndex].ConvertToString()}: {requirementPassed}");
            });

        }
    }
    private void Start() 
    {
        _requirements = RequirementGenerator.GenerateRequirements();
        GenerateCandidats(1);
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
        bool candidatProvidedResume = _candidats[_currentCandidat].candidateStats.WillProvideAResume();
        _visuals.SetResumeVisibility(candidatProvidedResume);
        if (candidatProvidedResume)
        {
            
        }
    }
}
