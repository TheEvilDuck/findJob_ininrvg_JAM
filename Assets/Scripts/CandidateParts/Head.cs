using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : CandidatePart
{
    [SerializeField]Transform _eyesPosition;
    public override void GenerateNext(BodyParts bodyParts)
    {
        int randomIndex = UnityEngine.Random.Range(0,bodyParts.eyes.GetLength(0));
        CandidatePart part = Instantiate(bodyParts.eyes[randomIndex],_eyesPosition.position,Quaternion.identity,_eyesPosition);
        part.GenerateNext(bodyParts);
    }
}
