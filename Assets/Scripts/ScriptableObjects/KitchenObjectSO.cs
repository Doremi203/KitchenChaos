using UnityEngine;

[CreateAssetMenu]
public class KitchenObjectSO : ScriptableObject
{
    [field: SerializeField] 
    public GameObject Prefab { get; set; }
    [field: SerializeField] 
    public Sprite Sprite { get; set; }
    [field: SerializeField] 
    public string ObjectName { get; set; }
}