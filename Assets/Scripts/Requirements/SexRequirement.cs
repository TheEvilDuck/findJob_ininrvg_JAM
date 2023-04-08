using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SexRequirement : IRequirement
{
    private static List<string>maleNames = new List<string>
    {
        "Петр","Олег","Абрахам","Георг","Петр II","Кирилл","Богдан","Евгений","Гриша","Семен","Уил","Александр","Глеб","Владимир"
    };
    private static List<string>femaleNames = new List<string>
    {
        "Алина","Василина","Анастасия","Екатерина","Анна","Снежанна","Анжела","Виктория"
    };
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

    public string GetResumeLine(CandidateStats candidateStats)
    {
        string result = $"Меня зовут {candidateStats.name}. ";
        if (candidateStats.isMale)
            return result+"Я мужчина. ";
        return result+"Я женщина. ";
    }
    public static string GenerateName(bool isMale)
    {
        if (isMale)
            return maleNames[UnityEngine.Random.Range(0,maleNames.Count)];
        return femaleNames[UnityEngine.Random.Range(0,femaleNames.Count)];
    }
}
