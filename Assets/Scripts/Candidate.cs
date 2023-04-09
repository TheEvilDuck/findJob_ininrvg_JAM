using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Candidate : MonoBehaviour
{
    public UnityEvent<Candidate>candidateClicked;
    public CandidateStats candidateStats;
    
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
    public void GenerateStats()
    {
        candidateStats = new CandidateStats();
        candidateStats.requirements = new List<IRequirement>();
        candidateStats.isMale = (UnityEngine.Random.Range(0,1f)>=0.5f);
        candidateStats.age = UnityEngine.Random.Range(5,100);
        candidateStats.requirements.Add(new AgeRequirement());
        candidateStats.requirements.Add(new SexRequirement());
        candidateStats.chanceToLie = UnityEngine.Random.Range(0,1f);
        candidateStats.iq = UnityEngine.Random.Range(2,300);
        candidateStats.name = SexRequirement.GenerateName(candidateStats.isMale);
        candidateStats.patience = UnityEngine.Random.Range(2,10);
        float degreeChance = UnityEngine.Random.Range(0,1f);
        candidateStats.degrees = new List<Degree>();
        if (degreeChance>=0.5f)
        {
            int degreesCount = UnityEngine.Random.Range(minInclusive: 1,6);
            for (int i = 0;i<degreesCount;i++)
            {
                Degree degree = DegreeRequirement.GenerateDegree();
                degree.ownerName = name;
                candidateStats.degrees.Add(degree);
                candidateStats.requirements.Add(new DegreeRequirement(degree));
            }
        }
       candidateStats.requirements.AddRange(RequirementGenerator.GenerateRandomRequirements(UnityEngine.Random.Range(0,5)));
    }
    public void RemoveCandidate()
    {
        Destroy(gameObject);
    }

}
