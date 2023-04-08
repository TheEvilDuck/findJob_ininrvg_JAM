using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandidateStats
{
    public int age;
    public bool isMale;
    public int iq;
    private float _chanceToLie = 1f;
    private float _chanceToForget = 0f;
    private  List<IRequirement> _requirements;

    public CandidateStats(List<IRequirement> requirements)
    {
        _requirements = requirements;
        age = UnityEngine.Random.Range(5,100);
        isMale = (UnityEngine.Random.Range(0,1f)>=0.5f);
        _chanceToLie = 1f;
        iq = UnityEngine.Random.Range(2,300);
    }
    public CandidateStats(CandidateStats toCopy)
    {
        age = toCopy.age;
        isMale = toCopy.isMale;
        iq = toCopy.iq;
    }
    public bool WillProvideAResume()
    {
        float chance = 1f;
        chance*=(1f-_chanceToForget);
        if (iq<50)
            chance*=0.5f;
        if (iq>=50)
            chance*=1.5f;
        if (chance>=0.5f)
            return true;
        return false;
    }
    public CandidateStats GetLiedStats()
    {
        if (_chanceToLie>=0)
        {
            Debug.Log("Сча проверим, нужно ли врать....");
            CandidateStats copy = new CandidateStats(this);
            if (_requirements!=null)
            {
                foreach(IRequirement requirement in _requirements)
                {
                    CandidateStats goal = requirement.GetIdealCandidateStats(this);
                    if (goal.age!=age)
                    {
                        Debug.Log($"Возраст не совпадает ({goal.age}), врет, реальный: {age}");
                        copy.age = goal.age;
                    }
                    if (goal.isMale!=isMale)
                    {
                        copy.isMale = goal.isMale;
                        Debug.Log(message: $"Пол не совпадает (мужской:{goal.isMale}), врет, реальный пол мужской: {isMale}");
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
