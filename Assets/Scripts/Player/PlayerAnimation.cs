using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovment))]
[RequireComponent (typeof(Player))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private PlayerMovment _playerMovment;
    private Player _player;

    private const string _moving = "Moving";
    private const string _cut = "Cut";

    public event UnityAction WheatCutted;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMovment = GetComponent<PlayerMovment>();
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.Cutting += Cut;
        _player.CuttingEnded += EndOfCutting;
    }

    private void OnDisable()
    {
        _player.Cutting -= Cut;
        _player.CuttingEnded -= EndOfCutting;
    }

    private void Update()
    {
        if (_playerMovment.Moving != _animator.GetBool(_moving))
        {
            _animator.SetBool(_moving, _playerMovment.Moving);
        }
    }

    private void Cut()
    {
        _animator.SetTrigger(_cut);
        //_animator.SetLayerWeight(1, 1);
    }

    public void CutoffMomentReached()
    {
        WheatCutted?.Invoke();
    }

    private void EndOfCutting()
    {
        //StartCoroutine(TurnOffCutting());
    }

    private IEnumerator TurnOffCutting()
    {
        float currentWeight = _animator.GetLayerWeight(1);
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);
        while (currentWeight > 0)
        {
            currentWeight -= 0.1f;
            _animator.SetLayerWeight(1, currentWeight);
            yield return waitForSeconds;
        }
    }

}
