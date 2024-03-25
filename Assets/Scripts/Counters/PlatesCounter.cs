using System;
using Interfaces;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateTaken;
    
    [SerializeField] private KitchenObjectSO _plateKitchenObjectSo;
    [SerializeField] private float _timeToSpawn = 4f;
    [SerializeField] private int _maxPlatesCount = 4;
    
    private float _plateSpawnTimer;
    private int _platesSpawnedCount;

    private void Update()
    {
        if (_platesSpawnedCount >= _maxPlatesCount)
            return;

        _plateSpawnTimer += Time.deltaTime;
        
        if (!(_plateSpawnTimer >= _timeToSpawn)) 
            return;
        
        _plateSpawnTimer = 0f;
        ++_platesSpawnedCount;
        OnPlateSpawned?.Invoke(this, EventArgs.Empty);
    }

    public override void Interact(Player player)
    {
        if ((player as IKitchenObjectHolder).HasKitchenObject)
            return;
        if (_platesSpawnedCount == 0)
            return;
        
        --_platesSpawnedCount;
        KitchenObject.SpawnKitchenObject(_plateKitchenObjectSo, player);
        OnPlateTaken?.Invoke(this, EventArgs.Empty);
    }
}