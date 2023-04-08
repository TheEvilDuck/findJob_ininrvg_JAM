using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RequirementGenerator
{
    public static IRequirement[] GenerateRequirements()
    {
        List<IRequirement>requirements = new List<IRequirement>();

        float ageNecessaryChance = UnityEngine.Random.Range(0.5f,1f);

        if (ageNecessaryChance>=0.5f)
        {
            AgeRequirement ageRequirement = new AgeRequirement();
            ageRequirement.GenerateRequirement();
            requirements.Add(ageRequirement);
        }
        int requirementsCount = UnityEngine.Random.Range(1,10);
        for (int i = 0;i<requirementsCount;i++)
        {

        }

        return requirements.ToArray();
    }
}
