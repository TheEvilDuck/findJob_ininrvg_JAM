using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

public class Visuals : MonoBehaviour
{
    [SerializeField]Transform _requirementsLayout;
    [SerializeField]UISelector _selectorPrefab;
    [SerializeField]Resume _resume;
    [SerializeField]Transform _inventory;
    [SerializeField]GameObject _resumeButton;
    [SerializeField]TextMeshProUGUI _pointsText;

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
    public void UpdateResume(CandidateStats candidateStats,string positionName)
    {
        _resume.UpdateResume(candidateStats,positionName);
    }
    public void UpdatePointsText(int points)
    {
        _pointsText.text = points.ToString();
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
