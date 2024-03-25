using UnityEngine;

[CreateAssetMenu]
public class AudioClipRefSO : ScriptableObject
{
    public AudioClip[] _chop;
    public AudioClip[] _deliveryFailure;
    public AudioClip[] _deliverySuccess;
    public AudioClip[] _footstep;
    public AudioClip[] _objectDrop;
    public AudioClip[] _objectPickup;
    public AudioClip _stoveSizzle;
    public AudioClip[] _trash;
    public AudioClip[] _warning;
}