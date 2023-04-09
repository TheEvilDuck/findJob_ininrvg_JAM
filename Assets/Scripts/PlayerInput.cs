using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerInput : MonoBehaviour
{
    public UnityEvent<int> playerClicked = new UnityEvent<int>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            playerClicked?.Invoke(0);
        if (Input.GetMouseButtonDown(1))
            playerClicked?.Invoke(1);
        if (Input.GetMouseButtonDown(2))
            playerClicked?.Invoke(2);
    }
}
