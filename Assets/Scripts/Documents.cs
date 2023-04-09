using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Documents : MonoBehaviour
{
    [SerializeField]Transform _uI;
    [SerializeField]Transform _inventory;
    [SerializeField]Button _documentButton;
    [SerializeField]Document _passport;
    [SerializeField]Document _degreeSertificate;


    private bool _passportGenerated = false;
    private List<Degree>_certificates = new List<Degree>();
    public bool GeneratePassport(CandidateStats candidateStats)
    {
        if (_passportGenerated)
            return false;
        _passportGenerated = true;
        GenerateDocument(_passport, candidateStats);
        return true;
    }
    public bool GenerateDegreeCertificate(Degree degree)
    {
        if (_certificates.Contains(degree))
            return false;
        
        CandidateStats candidateStats = new CandidateStats();
        candidateStats.degrees = new List<Degree>();
        candidateStats.degrees.Add(degree);
        GenerateDocument(_degreeSertificate,candidateStats);
        return true;
    }
    private void GenerateDocument(Document prefab, CandidateStats candidateStats)
    {
        Button button = Instantiate(_documentButton,_inventory);
        Document document = Instantiate(prefab,_uI);
        button.onClick.AddListener(document.Enable);
        document.FeelFields(candidateStats);
    }
}
