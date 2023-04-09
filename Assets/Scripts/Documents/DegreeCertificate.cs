using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DegreeCertificate : Document
{
    [SerializeField]TextMeshProUGUI _degreePlace;
    [SerializeField]TextMeshProUGUI _degreeName;
    [SerializeField]TextMeshProUGUI _ownerName;
    public override void FeelFields(CandidateStats candidateStats)
    {
        _degreeName.text = candidateStats.degrees[0].degreeName;
        _degreePlace.text = candidateStats.degrees[0].placeName;
        _ownerName.text = candidateStats.degrees[0].ownerName;
    }
}
