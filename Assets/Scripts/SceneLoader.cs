using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]string _scene;

    public void LoadScene()
    {
        SceneManager.LoadScene(_scene);
    }
}
