using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Degree
{
    public string degreeName;
    public string placeName;
    public string ownerName;
    public int year;
}
public class DegreeRequirement : IRequirement
{
    Degree _degree = new Degree();

    private static List<string>degreeNames = new List<string>
    {
        "Плотник","Кожевник","Боксер","Ученый","Прикладная информатика","Слесарь 94 разряда","Овощевод","Олег","Алхимик","Воин"
    };
    private static List<string>placeNames = new List<string>
    {
        "Хорвардс","ДВФУ","Гарврард","МГБОУ ГЫГДОШ ДШхъэМАМ имени Олега","Онлайн школа Навык-коробка"
    };
    public bool CompareRequirement(CandidateStats candidateStats)
    {
        foreach (Degree degree in candidateStats.degrees)
        {
            if (degree.degreeName==_degree.degreeName&&
                degree.year<=_degree.year)
                return true;
        }
        return false;
    }

    public string ConvertToString()
    {
        return $"Требуемое образование: {_degree.degreeName}, желательно в {_degree.placeName}(от {2023-_degree.year} лет)";
    }

    public void GenerateRequirement()
    {
        _degree = GenerateDegree();
    }

    public CandidateStats GetIdealCandidateStats(CandidateStats current)
    {
        CandidateStats goalStats = new CandidateStats(current);
        Degree degree = _degree;
        degree.ownerName = goalStats.name;
        goalStats.degrees.Add(degree);
        return goalStats;
    }

    public string GetResumeLine(CandidateStats candidateStats)
    {
        string result = "Есть образование по специальности ";
        foreach (Degree degree in candidateStats.degrees)
        {
            if (degree.placeName==_degree.placeName&&degree.degreeName==_degree.degreeName
            &&degree.ownerName==_degree.ownerName&&degree.year==_degree.year)
                result+=$"{degree.degreeName} в {degree.placeName}({2023-degree.year} лет), ";
            else
            result = "";
        }
        return result;
    }
    public static Degree GenerateDegree()
    {
        Degree degree= new Degree();
        degree.year = UnityEngine.Random.Range(2011,2022);
        degree.placeName = placeNames[UnityEngine.Random.Range(0,placeNames.Count)];
        degree.degreeName = degreeNames[UnityEngine.Random.Range(0,degreeNames.Count)];
        return degree;
    }
    public DegreeRequirement(Degree degree)
    {
        _degree = degree;
    }
}
