using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;


[System.Serializable]
public struct CandidateStats
{
    public int age;
    public bool isMale;
    public int iq;
    public string name;
    public float chanceToLie;
    public float chanceToForget;
    public  List<IRequirement> requirements;

    public List<Degree>degrees;

    public static CandidateStats DeepCopy(CandidateStats candidateStats)
    {
        BinaryFormatter s = new BinaryFormatter();
        using (MemoryStream ms = new MemoryStream())
        {
            s.Serialize(ms, candidateStats);
            ms.Position = 0;
            CandidateStats t = (CandidateStats)s.Deserialize(ms);

            return t;
        }
    }
    public bool WillProvideAResume()
    {
        float chance = 1f;
        chance*=(1f-chanceToForget);
        if (iq<50)
            chance*=0.5f;
        if (iq>=50)
            chance*=1.5f;
        if (chance>=0.5f)
            return true;
        return false;
    }
    public CandidateStats GetLiedStats(List<IRequirement> requirements)
    {
        if (chanceToLie>=0.5f)
        {
            Debug.Log("Сча проверим, нужно ли врать....");
            CandidateStats copy = DeepCopy(this);
            if (requirements!=null)
            {
                foreach(IRequirement requirement in requirements)
                {
                    if (!requirement.CompareRequirement(copy))
                    {
                        Debug.Log($"Врет о {requirement.ConvertToString()}, на самом деле {requirement.CompareRequirement(copy)}");
                        copy = DeepCopy(requirement.GetIdealCandidateStats(copy));
                    }
                }
            }
            return copy;
        }
        else
        {
            return this;
        }
    }
}
