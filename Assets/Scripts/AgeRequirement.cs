using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgeRequirement : IRequirement
{
    int _minAge;
    int _maxAge;

    bool _hasFloor = false;

    public bool CompareRequirement(CandidateStats candidateStats)
    {
        if (candidateStats.age>=_minAge)
        {
            if (_hasFloor)
            {
                return candidateStats.age<=_maxAge;
            }
            return true;
        }
        return false;
    }

    public string ConvertToString()
    {
        string result = _minAge.ToString();
        if (_hasFloor)
        {
            result+=("-"+_maxAge.ToString());
            result = "Возраст: "+result;
        }
        else
            result = "Mинимальный возраст: "+result;
        return result;
    }

    public void GenerateRequirement()
    {
        _minAge = UnityEngine.Random.Range(14,99);
        float ageCeilChance = UnityEngine.Random.Range(0,1f);
        if (ageCeilChance>=0.5f)
        {
            _hasFloor = true;
            int max = UnityEngine.Random.Range(14,99);
            _maxAge = Mathf.Max(_minAge,max);
            _minAge = Mathf.Min(_minAge,max);

            if (_minAge==_maxAge)
                _maxAge++;
        }
    }
}
