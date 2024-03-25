using System;
using Unity.VisualScripting;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool _isFirstUpdate = true;

    private void Update()
    {
        if (_isFirstUpdate) 
            Loader.LoaderCallback();
        
        _isFirstUpdate = false;
    }
}