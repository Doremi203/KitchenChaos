using UnityEngine;

[CreateAssetMenu]
public class FryingRecipeSO : ScriptableObject
{
    [field: SerializeField] 
    public KitchenObjectSO Input { get; set; }
    [field: SerializeField] 
    public KitchenObjectSO Output { get; set; }
    [field: SerializeField] 
    public float TimeToFry { get; set; }
}