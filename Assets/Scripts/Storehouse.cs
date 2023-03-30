using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Storehouse : MonoBehaviour
{
    public event UnityAction BlockBuyed;

    [SerializeField] private float _interval;
    [SerializeField] private int _reward;
    [SerializeField] private Backpack _backpack;
    [SerializeField] private Transform _spawnerContainer;
    [SerializeField] private Transform _storagePoint;
    private Coroutine _currentSell;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _currentSell = StartCoroutine(BuyBlocks());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            if(_currentSell != null)
            {
                StopCoroutine(_currentSell);
            }
        }
    }

    private IEnumerator BuyBlocks()
    {
        WaitForSeconds interval = new WaitForSeconds(_interval);
        int count = _backpack.CurrentAmount;
        List<WheatBlock> wheatBlocs = new List<WheatBlock>();

        for (int i = 0; i < count; i++)
        {
            _backpack.SellBlock(_reward, out WheatBlock block);
            block.MoveBlock(_storagePoint, _spawnerContainer, Vector3.zero);
            wheatBlocs.Add(block);
            BlockBuyed?.Invoke();
            yield return interval;
        }

        foreach(WheatBlock wheat in wheatBlocs)
        {
            wheat.gameObject.SetActive(false);
        }
    }
}
