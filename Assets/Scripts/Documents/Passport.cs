using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Passport : Document
{
    [SerializeField]TextMeshProUGUI _name;
    [SerializeField]TextMeshProUGUI _sex;

    public override void FeelFields(CandidateStats candidateStats)
    {
        _name.text = "Имя: "+candidateStats.name;
        string sex = "Женский";
        if (candidateStats.isMale)
            sex = "Мужской";
        _sex.text = "Пол: "+sex;
    }
}
