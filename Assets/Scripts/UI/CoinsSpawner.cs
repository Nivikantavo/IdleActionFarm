using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.Events;

public class CoinsSpawner : ObjectPool
{
    public event UnityAction CoinDelivered;

    [SerializeField] private GameObject _spawnPrefab;
    [SerializeField] private Storehouse _storehouse;
    [SerializeField] private Transform _moneyView;
    [SerializeField] private float _moveTime;

    private void Start()
    {
        Initialize(_spawnPrefab);
    }

    private void OnEnable()
    {
        _storehouse.BlockBuyed += OnBlockBuyed;
    }

    private void OnDisable()
    {
        _storehouse.BlockBuyed -= OnBlockBuyed;
    }

    private void OnBlockBuyed()
    {
        SpawnCoin();
    }

    private void SpawnCoin()
    {
        if (TryGetObject(out GameObject coin))
        {
            coin.transform.position = transform.position;
            coin.SetActive(true);
            StartCoroutine(MoveCoin(coin, _moneyView.position));
        }
    }

    private IEnumerator MoveCoin(GameObject coin, Vector3 position)
    {
        WaitForSeconds delay = new WaitForSeconds(_moveTime);

        coin.transform.DOMove(position, _moveTime);
        yield return delay;
        coin.SetActive(false);
        CoinDelivered?.Invoke();
    }
}
