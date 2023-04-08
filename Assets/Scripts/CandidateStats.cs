using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandidateStats
{
    public int age
    {
        get;
        private set;
    }

    public CandidateStats()
    {
        age = UnityEngine.Random.Range(5,100);
    }
}
