using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class WheatBlocksSpawner : ObjectPool
{
    [SerializeField] private GameObject _spawnPrefab;
    [SerializeField] private List<GardenBed> _gardenBeds;

    private void Awake()
    {
        DOTween.SetTweensCapacity(700, 312);
    }

    private void Start()
    {
        Initialize(_spawnPrefab);
    }

    private void OnEnable()
    {
        foreach(var gardenBed in _gardenBeds)
        {
            gardenBed.WheatCutted += OnWheatCutted;
        }
    }

    private void OnDisable()
    {
        foreach (var gardenBed in _gardenBeds)
        {
            gardenBed.WheatCutted -= OnWheatCutted;
        }
    }

    private void OnWheatCutted(Vector3 spawnPosition)
    {
        SpawnWheatBlock(spawnPosition);
    }

    private void SpawnWheatBlock(Vector3 spawnPosition)
    {
        if(TryGetObject(out GameObject wheatBlock))
        {
            wheatBlock.transform.position = spawnPosition;
            wheatBlock.SetActive(true);
        }
    }
}
