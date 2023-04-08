using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Visuals : MonoBehaviour
{
    [SerializeField]Transform _requirementsLayout;
    [SerializeField]UISelector _selectorPrefab;
    [SerializeField]Resume _resume;
    [SerializeField]Transform _inventory;
    [SerializeField]GameObject _resumeButton;

    public UnityEvent<bool,int> CreateSelector(IRequirement requirement, int index)
    {
        UISelector selector = Instantiate(_selectorPrefab,_requirementsLayout);
        selector.ConnectVisuals(requirement.ConvertToString(),index);
        return selector.objectSelected;
    }
    public void SetResumeVisibility(bool value)
    {
        _resumeButton.SetActive(value);
    }
    public void UpdateResume(CandidateStats candidateStats)
    {
        _resume.UpdateResume(candidateStats);
    }
}
