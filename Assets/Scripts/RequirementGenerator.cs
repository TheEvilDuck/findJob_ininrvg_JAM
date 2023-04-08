using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RequirementGenerator
{
    public static List<IRequirement> GenerateRequirements()
    {
        List<IRequirement>requirements = new List<IRequirement>();

        float ageNecessaryChance = UnityEngine.Random.Range(0f,1f);
        float sexNecessaryChance = UnityEngine.Random.Range(0f,1f);
        float degreeChance = UnityEngine.Random.Range(0f,1f);

        if (ageNecessaryChance>=0.4f)
        {
            AgeRequirement ageRequirement = new AgeRequirement();
            ageRequirement.GenerateRequirement();
            requirements.Add(ageRequirement);
        }
        if (sexNecessaryChance>=0.5f)
        {
            SexRequirement sexRequirement= new SexRequirement();
            sexRequirement.GenerateRequirement();
            requirements.Add(sexRequirement);
        }
        if (degreeChance>=0.5f)
        {
            int requirementsCount = UnityEngine.Random.Range(1,maxExclusive: 3);
            for (int i = 0;i<requirementsCount;i++)
            {
                DegreeRequirement degreeRequirement = new DegreeRequirement(DegreeRequirement.GenerateDegree());
                requirements.Add(degreeRequirement);
            }
        }
        return requirements;
    }
    public static List<IRequirement> GenerateRandomRequirements(int count)
    {
        List<IRequirement>requirements = new List<IRequirement>();
        for (int i = 0;i<count;i++)
        {
            
        }

        return requirements;
    }
}
