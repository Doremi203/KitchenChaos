using Interfaces;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image _barImage;
    private IHasProgress _cuttingCounter;

    private void Start()
    {
        _cuttingCounter = GetComponentInParent<IHasProgress>();
        _barImage.fillAmount = 0f;
        _cuttingCounter.OnProgressChanged += (_, progress) =>
        {
            _barImage.fillAmount = progress;
            gameObject.SetActive(!(progress == 0f || progress == 1f));
        };
        gameObject.SetActive(false);
    }
}