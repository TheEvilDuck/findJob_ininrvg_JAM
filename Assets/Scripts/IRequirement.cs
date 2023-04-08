using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRequirement
{
    public void GenerateRequirement();
    public string ConvertToString();
    public bool CompareRequirement(CandidateStats candidateStats);
    public CandidateStats GetIdealCandidateStats(CandidateStats current);
}
