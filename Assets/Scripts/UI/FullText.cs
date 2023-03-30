using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FullText : MonoBehaviour
{
    [SerializeField] private Backpack _backpack;

    private Animator _animator;

    private const string _backpackFull = "BackpackFull";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _backpack.BackpackFull += OnBackpackFull;
    }

    private void OnDisable()
    {
        _backpack.BackpackFull -= OnBackpackFull;
    }

    public void OnBackpackFull()
    {
        _animator.SetTrigger(_backpackFull);
    }
}
