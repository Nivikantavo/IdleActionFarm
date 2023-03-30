using System.Collections;
using TMPro;
using UnityEngine;

public class MoneyViewer : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private CoinsSpawner _coinsSpawner;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private float _addingDelay;

    private const string _rewarding = "Rewarding";
    private int _currentValue;
    private Coroutine _viewCoroutine;
    private Animator _viewAnimator;

    private void Awake()
    {
        _viewAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        _currentValue = _wallet.Money;
        _moneyText.text = _currentValue.ToString();
    }

    private void OnEnable()
    {
        _coinsSpawner.CoinDelivered += OnCoinDelivered;
    }

    private void OnDisable()
    {
        _coinsSpawner.CoinDelivered -= OnCoinDelivered;
    }

    private IEnumerator ViewBalance()
    {
        WaitForSeconds delay = new WaitForSeconds(_addingDelay);

        

        while (_currentValue < _wallet.Money)
        {
            _currentValue++;
            _moneyText.text = _currentValue.ToString();
            yield return delay;
        }
    }

    private void OnCoinDelivered()
    {
        _viewAnimator.SetTrigger(_rewarding);

        if (_viewCoroutine != null)
        {
            StopCoroutine(_viewCoroutine);
        }

        _viewCoroutine = StartCoroutine(ViewBalance());
    }
}
