using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform _plateSpawnPoint;
    [SerializeField] private GameObject _plateVisualPrefab;

    private List<GameObject> _platesSpawned;
    private PlatesCounter _platesCounter;
    private Vector3 _curSpawnOffset;
    private const float Offset = 0.1f;

    private void Start()
    {
        _platesSpawned = new List<GameObject>();
        _platesCounter = GetComponentInParent<PlatesCounter>();
        _platesCounter.OnPlateSpawned += (_, _) =>
        {
            _platesSpawned.Add(Instantiate(_plateVisualPrefab, _plateSpawnPoint));
            _platesSpawned[^1].transform.localPosition = _curSpawnOffset;
            _curSpawnOffset += Vector3.up * Offset;
        };
        _platesCounter.OnPlateTaken += (_, _) =>
        {
            var lastPlate = _platesSpawned[^1];
            Destroy(lastPlate);
            _platesSpawned.Remove(lastPlate);
            _curSpawnOffset += Vector3.down * Offset; 
        };
    }
}