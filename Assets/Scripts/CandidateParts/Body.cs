using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : CandidatePart
{
    [SerializeField]Transform _neckPosition;
    [SerializeField]Transform _clothPosition;
    public override void GenerateNext(BodyParts bodyParts)
    {
        int randomIndex = UnityEngine.Random.Range(0,bodyParts.heads.GetLength(0));
        CandidatePart part = Instantiate(bodyParts.heads[randomIndex],_neckPosition.position,Quaternion.identity,_neckPosition);
        part.GenerateNext(bodyParts);
    }
}
