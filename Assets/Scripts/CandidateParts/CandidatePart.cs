using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CandidatePart : MonoBehaviour
{
    public abstract void GenerateNext(BodyParts bodyParts);
}
