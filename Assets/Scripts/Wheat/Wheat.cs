using EzySlice;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class Wheat : MonoBehaviour
{
    public event UnityAction<Vector3> Cutted;

    [SerializeField] private float _growthTime;
    [SerializeField] private Material _crossSectionMaterial;

    private MeshRenderer _renderer;
    private BoxCollider _boxCollider;
    private GameObject _hull;

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _renderer = GetComponent<MeshRenderer>();
    }

    public void Cut(SlicedHull hull)
    {
        Cutted?.Invoke(transform.position);

        if(_hull == null)
        {
            _hull = hull.CreateLowerHull(this.gameObject, _crossSectionMaterial);
            _hull.transform.parent = transform;
        }
        else
        {
            _hull.SetActive(true);
        }
        _hull.transform.position = transform.position;

        StartCoroutine(Growth());

        _renderer.enabled = false;
        _boxCollider.enabled = false;
    }

    private IEnumerator Growth()
    {
        WaitForSeconds growthDelay = new WaitForSeconds(_growthTime);

        yield return growthDelay;
        _renderer.enabled = true;
        _boxCollider.enabled = true;
        _hull.SetActive(false);
    }

}
