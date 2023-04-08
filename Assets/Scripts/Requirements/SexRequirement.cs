using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SexRequirement : IRequirement
{
    bool _male = false;
    public bool CompareRequirement(CandidateStats candidateStats)
    {
        return candidateStats.isMale==_male;

    }

    public string ConvertToString()
    {
        string sex = "Женский";
        if (_male)
            sex = "Мужской";
        return ($"Пол: {sex}");
    }

    public void GenerateRequirement()
    {
        float random = UnityEngine.Random.Range(0,1f);
        _male = (random>=0.5f);
    }

    public CandidateStats GetIdealCandidateStats(CandidateStats current)
    {
       CandidateStats goalStats = new CandidateStats(current);
       goalStats.isMale = _male;
       return goalStats;
    }
}
