using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Document : MonoBehaviour
{
    private bool _isHide = true;
    private Vector3 _position;
    public abstract void FeelFields(CandidateStats candidateStats);
    public void HideOrShow()
    {
        if (_isHide)
            transform.position = new Vector3(1000,1000,1000);
        else
            transform.position = _position;
    }
    public void Enable()
    {
        _isHide = !_isHide;
        HideOrShow();
    }
    private void OnEnable() {
        _position = transform.position;
        HideOrShow();
    }
}
