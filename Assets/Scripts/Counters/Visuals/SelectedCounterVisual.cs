using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter _baseCounter;
    [SerializeField] private GameObject[] _visualGameObjects;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += (_, counter) =>
        {
            // Show visual if counter is selected, hide otherwise
            foreach (var visualGameObject in _visualGameObjects) 
                visualGameObject.SetActive(_baseCounter == counter);
        };
    }
    
    
}