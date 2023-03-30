using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Backpack : MonoBehaviour
{
    public int CurrentAmount => _wheatBlocks.Count;
    public int Capacity => _capacity;

    public event UnityAction<int> FullnessChanged;
    public event UnityAction BackpackFull;

    [SerializeField] private int _capacity;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _storehouse;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private float _blocksDistance;

    private Vector3 _offset = Vector3.zero;
    private List<WheatBlock> _wheatBlocks;

    private void Awake()
    {
        _wheatBlocks = new List<WheatBlock>();
    }

    private void OnEnable()
    {
        _player.BlockPikedUp += OnBlockPikedUp;
    }

    private void OnDisable()
    {
        _player.BlockPikedUp -= OnBlockPikedUp;
    }

    private void OnBlockPikedUp(WheatBlock block)
    {
        TryAddBlock(block);
    }

    private void TryAddBlock(WheatBlock block)
    {
        if(CurrentAmount < _capacity)
        {
            _offset.y += _blocksDistance;
            block.MoveBlock(transform, transform, _offset);
            _wheatBlocks.Add(block);
            FullnessChanged?.Invoke(CurrentAmount);

            if(_wheatBlocks.Count == _capacity)
            {
                BackpackFull?.Invoke();
            }
        }
    }
    
    public void SellBlock(int reward, out WheatBlock block)
    {
        block = _wheatBlocks[_wheatBlocks.Count - 1];
        RemoveBlock();
        _wallet.AddMoney(reward);
    }

    private void RemoveBlock()
    {
        _wheatBlocks.Remove(_wheatBlocks[_wheatBlocks.Count - 1]);
        _offset.y -= _blocksDistance;
        FullnessChanged?.Invoke(CurrentAmount);
    }
}
