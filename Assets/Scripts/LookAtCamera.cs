using System;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted,
    }

    [SerializeField] private Mode _mode;
    private void LateUpdate()
    {
        switch (_mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main!.transform);
                break;
            case Mode.LookAtInverted:
                var position = transform.position;
                var invertedDir = position - Camera.main!.transform.position;
                transform.LookAt(position + invertedDir);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}