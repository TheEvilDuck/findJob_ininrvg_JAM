using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandidateStats
{
    public int age;
    public bool isMale;
    public int iq;
    public string name;
    private float _chanceToLie = 1f;
    private float _chanceToForget = 0f;
    public  List<IRequirement> requirements = new List<IRequirement>();

    public List<Degree>degrees = new List<Degree>();

    public CandidateStats()
    {
        age = UnityEngine.Random.Range(5,100);
        requirements.Add(new AgeRequirement());
        isMale = (UnityEngine.Random.Range(0,1f)>=0.5f);
        requirements.Add(new SexRequirement());
        _chanceToLie = UnityEngine.Random.Range(0,1f);
        iq = UnityEngine.Random.Range(2,300);
        name = SexRequirement.GenerateName(isMale);
        float degreeChance = UnityEngine.Random.Range(0,1f);
        if (degreeChance>=0.5f)
        {
            int degreesCount = UnityEngine.Random.Range(minInclusive: 1,6);
            for (int i = 0;i<degreesCount;i++)
            {
                Degree degree = DegreeRequirement.GenerateDegree();
                degree.ownerName = name;
                degrees.Add(degree);
                requirements.Add(new DegreeRequirement(degree));
            }
        }
        requirements.AddRange(RequirementGenerator.GenerateRandomRequirements(UnityEngine.Random.Range(0,5)));
        
    }
    public CandidateStats(CandidateStats toCopy)
    {
        age = toCopy.age;
        isMale = toCopy.isMale;
        iq = toCopy.iq;
        requirements = toCopy.requirements;
        name = toCopy.name;
        degrees = toCopy.degrees;
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
    public CandidateStats GetLiedStats(List<IRequirement> requirements)
    {
        if (_chanceToLie>=0.5f)
        {
            Debug.Log("Сча проверим, нужно ли врать....");
            CandidateStats copy = new CandidateStats(this);
            if (requirements!=null)
            {
                foreach(IRequirement requirement in requirements)
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
