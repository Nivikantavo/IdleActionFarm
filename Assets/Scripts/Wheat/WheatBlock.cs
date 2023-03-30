using UnityEngine;
using DG.Tweening;
using System.Collections;

public class WheatBlock : MonoBehaviour
{
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _moveTime;

    private BoxCollider _boxCollider;
    private Coroutine _coroutine;

    public void MoveBlock(Transform target, Transform parent, Vector3 offset)
    {
        _boxCollider.enabled = false;
        if (_coroutine == null)
        {
            StartCoroutine(MoveToTarget(target, parent, offset));
        }
    }

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        _boxCollider.enabled = true;
        StartJump();
    }

    private void OnDisable()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    private IEnumerator MoveToTarget(Transform target, Transform parent, Vector3 offset)
    {
        transform.parent = null;
        WaitForSeconds delay = new WaitForSeconds(_moveTime);

        transform.DOLocalJump(target.position + offset, _jumpPower, 1, _moveTime);
        transform.DORotateQuaternion(target.rotation, _moveTime);

        yield return delay;

        transform.SetPositionAndRotation(target.position + offset, target.rotation);
        transform.parent = parent;
        
    }

    private void StartJump()
    {   
        Vector3 offset = new Vector3(Random.value, 0, Random.value);
        transform.DOLocalJump(transform.position + offset, _jumpPower, 1, _moveTime);
    }
}
