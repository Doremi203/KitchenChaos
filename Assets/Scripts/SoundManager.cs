using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipRefSO _audioClipRefSo;
    private readonly float _volume = 1f;

    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySuccess += (_, _) => PlayRandomSoundFor(_audioClipRefSo._deliverySuccess, DeliveryCounter.Instance.transform.position, _volume);
        DeliveryManager.Instance.OnDeliveryFailure += (_, _) => PlayRandomSoundFor(_audioClipRefSo._deliveryFailure, DeliveryCounter.Instance.transform.position, _volume);
        
        CuttingCounter.OnAnyCut += (sender, _) =>
        {
            var cuttingCounter = sender as CuttingCounter;
            PlayRandomSoundFor(_audioClipRefSo._chop, cuttingCounter!.transform.position, _volume);
        };
        
        Player.Instance.OnPickedUpSomething += (_, _) =>
            PlayRandomSoundFor(_audioClipRefSo._objectPickup, Player.Instance.transform.position, _volume);
        
        BaseCounter.OnObjectPlaced += (sender, _) =>
        {
            var counter = sender as BaseCounter;
            PlayRandomSoundFor(_audioClipRefSo._objectDrop, counter!.transform.position, _volume);
        };

        TrashCounter.OnTrashed += (sender, _) =>
        {
            var trashCounter = sender as TrashCounter;
            PlayRandomSoundFor(_audioClipRefSo._trash, trashCounter!.transform.position, _volume);
        };

        PlayerSounds.OnStep += (sender, _) =>
        {
            var playerPos = (sender as PlayerSounds)!.transform.position;
            PlayRandomSoundFor(_audioClipRefSo._footstep, playerPos, _volume);
        };
    }

    private void PlayRandomSoundFor(AudioClip[] audioClips, Vector3 pos, float volume) 
        => AudioSource.PlayClipAtPoint(audioClips[Random.Range(0, audioClips.Length)], pos, volume);
}