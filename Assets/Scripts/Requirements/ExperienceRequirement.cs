using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class ExperienceRequirement : IRequirement
{
    public bool CompareRequirement(CandidateStats candidateStats)
    {
        throw new System.NotImplementedException();
    }

    public string ConvertToString()
    {
        throw new System.NotImplementedException();
    }

    public bool GenerateDocument(Documents documents, CandidateStats candidateStats)
    {
        throw new System.NotImplementedException();
    }

    public void GenerateRequirement()
    {
        throw new System.NotImplementedException();
    }

    public CandidateStats GetIdealCandidateStats(CandidateStats current)
    {
        throw new System.NotImplementedException();
    }

    public string GetResumeLine(CandidateStats candidateStats)
    {
        throw new System.NotImplementedException();
    }
}
