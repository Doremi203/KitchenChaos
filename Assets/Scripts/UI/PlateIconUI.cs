using UnityEngine;
using UnityEngine.UI;

public class PlateIconUI : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSo) 
        => _image.sprite = kitchenObjectSo.Sprite;
}