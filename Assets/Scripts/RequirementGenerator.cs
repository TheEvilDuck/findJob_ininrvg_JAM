using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RequirementGenerator
{
    public static List<IRequirement> GenerateRequirements()
    {
        List<IRequirement>requirements = new List<IRequirement>();

        float ageNecessaryChance = UnityEngine.Random.Range(0.5f,1f);
        float sexNecessaryChance = UnityEngine.Random.Range(0.5f,1f);

        if (ageNecessaryChance>=0f)
        {
            AgeRequirement ageRequirement = new AgeRequirement();
            ageRequirement.GenerateRequirement();
            requirements.Add(ageRequirement);
        }
        if (sexNecessaryChance>=0f)
        {
            SexRequirement sexRequirement= new SexRequirement();
            sexRequirement.GenerateRequirement();
            requirements.Add(sexRequirement);
        }
        int requirementsCount = UnityEngine.Random.Range(1,10);
        for (int i = 0;i<requirementsCount;i++)
        {

        }

        return requirements;
    }
}
