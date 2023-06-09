using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SoftSkillsRequirement : IRequirement
{
    string _softSkill;
    private List<string>_softSkills = new List<string>
    {
        "Стрессоустойчивость", "Умение работать в команде","Приятно пахнет из рта","Связная речь","Против алкоголя","Жизнерадостность","Целеустремленность","В семье нет Олегов",
        "Любовь к животным","Наличие третьего глаза","Чистая аура","Неприязнь к грязи","Неприязнь к солнечному свету","Аллергия на усталость","Руки-базуки",
        "Невероятная любовь к сыру","Озабоченность всем вокруг","Крепкое рукопожатие"
    };
    public bool CompareRequirement(CandidateStats candidateStats)
    {
        foreach (IRequirement requirement in candidateStats.requirements)
        {
            if (requirement.ConvertToString()==this.ConvertToString())
                return true;
        }
        return false;
    }

    public string ConvertToString()
    {
        return _softSkill;
    }

    public bool GenerateDocument(Documents documents, CandidateStats candidateStats)
    {
        throw new System.NotImplementedException();
    }

    public void GenerateRequirement()
    {
        _softSkill = _softSkills[UnityEngine.Random.Range(0,_softSkills.Count)];
    }

    public CandidateStats GetIdealCandidateStats(CandidateStats current)
    {
        CandidateStats goalStats = CandidateStats.DeepCopy(current);
        if (goalStats.requirements==null)
            goalStats.requirements = new List<IRequirement>();
        goalStats.requirements.Add(this);
        return goalStats;
    }

    public string GetResumeLine(CandidateStats candidateStats)
    {
        return _softSkill+". ";
    }
}
