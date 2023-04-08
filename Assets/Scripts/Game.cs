using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private CandidateStats[] _candidats;
    private IRequirement[] _requirements;
    private void GenerateCandidats(int count)
    {
        _candidats = new CandidateStats[count];
        for (int i = 0;i<count;i++)
        {
            _candidats[i] = new CandidateStats();
        }
    }
    private void Start() 
    {
        GenerateCandidats(5);
        _requirements = RequirementGenerator.GenerateRequirements();
        Debug.Log($"Requirements count: {_requirements.Length}");
        foreach (IRequirement requirement in _requirements)
        {
            Debug.Log(requirement.ConvertToString());
        }
    }
}
