using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BackpackAnimation : MonoBehaviour
{
    [SerializeField] private PlayerMovment _playerMovment;

    private Animator _animator;
    private const string _moving = "Moving";


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_playerMovment.Moving != _animator.GetBool(_moving))
        {
            _animator.SetBool(_moving, _playerMovment.Moving);
        }
    }
}
