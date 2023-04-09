using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Degree
{
    public string degreeName;
    public string placeName;
    public string ownerName;
    public int year;
}
[System.Serializable]
public class DegreeRequirement : IRequirement
{
    Degree _degree = new Degree();

    private static List<string>degreeNames = new List<string>
    {
        "Плотник","Кожевник","Боксер","Ученый","Прикладная информатика","Слесарь 94 разряда","Овощевод","Олег","Алхимик","Воин",
        "Верховный жрец","Бейсболист","Повар","Маньяк","Джейсон Стейтем","Натуралист","Тайный продавец","Властелин морей"
    };
    private static List<string>placeNames = new List<string>
    {
        "Хорвардс","ДВФУ","Гарврард","МГБОУ ГЫГДОШ ДШхъэМАМ имени Олега","Онлайн школа Навык-коробка",
        "Высшая школа волшебства","Шарага за поворотом","МБИК мьау ООООООО","Лондонская академика академиков","Курсы по замозащите",
        "Онлайн курсы по ракетостроению","Онлайн курсы по мировому господству"
    };
    public bool CompareRequirement(CandidateStats candidateStats)
    {
        if (candidateStats.degrees==null)
            return false;
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
        CandidateStats goalStats = CandidateStats.DeepCopy(current);
        Degree degree = _degree;
        degree.ownerName = goalStats.name;
        if (goalStats.requirements==null)
            goalStats.requirements = new List<IRequirement>();
        if (goalStats.degrees==null)
            goalStats.degrees = new List<Degree>();
        goalStats.degrees.Add(degree);
        DegreeRequirement degreeRequirement = new DegreeRequirement(degree);
        goalStats.requirements.Add(degreeRequirement);
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

    public bool GenerateDocument(Documents documents, CandidateStats candidateStats)
    {
        if (candidateStats.chanceToForget<=0.1f)
            return false;
        
        if (!CompareRequirement(candidateStats))
            return false;
        Degree degree = new Degree();
        degree.degreeName = _degree.degreeName;
        degree.placeName = _degree.placeName;
        degree.year = _degree.year;
        degree.ownerName = candidateStats.name;
        return documents.GenerateDegreeCertificate(degree);
    }

    public DegreeRequirement(Degree degree)
    {
        _degree = degree;
    }
}
