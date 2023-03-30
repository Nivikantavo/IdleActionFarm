using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GardenBed : MonoBehaviour
{
    public int Capacity { get; private set; }

    public event UnityAction<Vector3> WheatCutted;

    [SerializeField] private int _columns;
    [SerializeField] private int _rows;
    [SerializeField] private GameObject _wheatPrefab;
    [SerializeField] private float _padding;

    private List<Wheat> _wheats;


    private void Awake()
    {
        Initialize();
        Capacity = _columns * _rows;
    }

    private void OnDisable()
    {
        foreach (var wheat in _wheats)
        {
            wheat.Cutted -= OnWheatCutted;
        }
    }

    private void Initialize()
    {
        _wheats = new List<Wheat>();
        Vector3 spawnPoint = transform.position;
        for (int i = 0; i < _columns; i++)
        {
            for (int j = 0; j < _rows; j++)
            {
                Wheat wheat = Instantiate(_wheatPrefab, spawnPoint, transform.rotation, transform).GetComponent<Wheat>();
                _wheats.Add(wheat);
                wheat.Cutted += OnWheatCutted;

                spawnPoint.x += _padding;
            }
            spawnPoint.z += _padding;
            spawnPoint.x -= _padding * _rows;
        }
    }

    private void OnWheatCutted(Vector3 wheatPosition)
    {
        WheatCutted?.Invoke(wheatPosition);

    }
}
