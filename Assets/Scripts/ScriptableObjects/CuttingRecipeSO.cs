using UnityEngine;

[CreateAssetMenu]
public class CuttingRecipeSO : ScriptableObject
{
    [field: SerializeField] 
    public KitchenObjectSO Input { get; set; }
    [field: SerializeField] 
    public KitchenObjectSO Output { get; set; }
    [field: SerializeField] 
    public int CutsToDo { get; set; }
}