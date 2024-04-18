﻿using Assets.__Game.Scripts.Fish;
using Assets.__Game.Scripts.Tools;
using UnityEngine;

namespace Assets.__Game.Scripts.LevelItems
{
  public class FishSpawner : MonoBehaviour
  {
    [SerializeField] private float _minFishMovementSpeed;
    [SerializeField] private float _maxFishMovementSpeed;

    [Space]
    [SerializeField] private FishSpawnInfo[] _fishToSpawn;

    private RandomPointInCamera _randomPointInCamera;

    private void Awake()
    {
      _randomPointInCamera = new RandomPointInCamera(Camera.main);
    }

    private void Start()
    {
      SpawnFish();
    }

    private void SpawnFish()
    {
      float randSpeed = Random.Range(_minFishMovementSpeed, _maxFishMovementSpeed);

      foreach (var fishInfo in _fishToSpawn)
      {
        for (int i = 0; i < fishInfo.Amount; i++)
        {
          Vector3 point = _randomPointInCamera.GetRandomPointInCamera();
          Vector3 spawnPosition = new(point.x, point.y, 0);
          Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0, 2) == 0 ? 90f : -90f, 0f);

          GameObject spawnedFish = Instantiate(fishInfo.FishContainerSo.GetRandomFish(),
            spawnPosition, randomRotation);
          FishHandler fishHandler = spawnedFish.GetComponent<FishHandler>();
          FishMovement fishMovement = spawnedFish.GetComponent<FishMovement>();

          fishHandler.SetFishNumber(fishInfo.FishNumber);
          fishMovement.SetParameters(randSpeed);
        }
      }
    }
  }
}