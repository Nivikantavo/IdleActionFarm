using EzySlice;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerAnimation))]
public class Player : MonoBehaviour
{
    public event UnityAction Cutting;
    public event UnityAction CuttingEnded;
    public event UnityAction<WheatBlock> BlockPikedUp;

    [SerializeField] private GameObject _plane;

    private SlicePlane _slicePlane;
    private PlayerAnimation _playerAnimation;

    private List<GameObject> _currentWheat;

    private void Awake()
    {
        _currentWheat = new List<GameObject>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _slicePlane = _plane.GetComponent<SlicePlane>();
    }

    private void OnEnable()
    {
        _playerAnimation.WheatCutted += OnWheatCutted;
        _slicePlane.WheatFounded += OnWheatFind;
        _slicePlane.WheatLost += OnWheatLost;
    }

    private void OnDisable()
    {
        _slicePlane.WheatFounded -= OnWheatFind;
        _playerAnimation.WheatCutted -= OnWheatCutted;
        _slicePlane.WheatLost += OnWheatLost;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<WheatBlock>(out WheatBlock block))
        {
            BlockPikedUp?.Invoke(block);
        }
    }

    private void OnWheatFind(GameObject wheat)
    {
        if(_currentWheat.Contains(wheat) == false)
            _currentWheat.Add(wheat);
        Cut();
    }

    private void OnWheatLost(GameObject wheat)
    {
        if (_currentWheat.Contains(wheat) == true)
            _currentWheat.Remove(wheat);
    }

    private void Cut()
    {
        Cutting?.Invoke();
    }

    private void OnWheatCutted()
    {
        if(_currentWheat.Count == 0)
        {
            return;
        }

        EzySlice.Plane slicePlane = new EzySlice.Plane();
        slicePlane.Compute(_plane);
        foreach(var wheat in _currentWheat)
        {
            SlicedHull hull = SliceObject(wheat);

            if (hull != null)
            {
                wheat.GetComponent<Wheat>().Cut(hull);
            }
        }
        _currentWheat.Clear();
        CuttingEnded?.Invoke();
    }

    public SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(_plane.transform.position, transform.up, crossSectionMaterial);
    }
}
