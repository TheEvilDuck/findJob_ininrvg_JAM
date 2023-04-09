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
    private List<UISelector>uISelectors = new List<UISelector>();

    public UnityEvent<bool,int> CreateSelector(IRequirement requirement, int index)
    {
        UISelector selector = Instantiate(_selectorPrefab,_requirementsLayout);
        uISelectors.Add(selector);
        selector.ConnectVisuals(requirement.ConvertToString(),index);
        selector.objectSelected.AddListener((bool selected,int index)=>
        {
            if (!selected)
                return;
            foreach (UISelector uISelector in uISelectors)
            {
                if (uISelector.index!=index)
                    uISelector.ForceState(false);
            }
        });
        return selector.objectSelected;
    }
    public void SetResumeVisibility(bool value)
    {
        _resumeButton.SetActive(value);
        if (!value)
        {
            _resume.Enable(value);
        }
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
