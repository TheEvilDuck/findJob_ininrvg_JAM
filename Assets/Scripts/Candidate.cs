using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Candidate : MonoBehaviour
{
    public UnityEvent<Candidate>candidateClicked;
    public CandidateStats candidateStats
    {
        get;
        private set;
    }
    
    private void Awake() 
    {
        candidateClicked = new UnityEvent<Candidate>();
    }
    public void GenerateVisuals(BodyParts bodyParts, Transform parent)
    {
       int randomIndex = UnityEngine.Random.Range(0,bodyParts.bodies.GetLength(0));
       CandidatePart part = Instantiate(bodyParts.bodies[randomIndex], parent.position,Quaternion.identity,parent);
       part.GenerateNext(bodyParts);
    }
    private void OnMouseDown() {
        candidateClicked?.Invoke(this);
    }
    public void GenerateStats(List<IRequirement> requirements)
    {
        candidateStats = new CandidateStats();
    }
    public void RemoveCandidate()
    {
        Destroy(gameObject);
    }

}
