using UnityEngine;

[CreateAssetMenu]
public class BurningRecipeSO : ScriptableObject
{
    [field: SerializeField] 
    public KitchenObjectSO Input { get; set; }
    [field: SerializeField] 
    public KitchenObjectSO Output { get; set; }
    [field: SerializeField] 
    public float TimeToBurn { get; set; }
}