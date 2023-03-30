using UnityEngine;
using UnityEngine.Events;

public class RayScan : MonoBehaviour
{
    [SerializeField] private int _rays;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _angle;
    [SerializeField] private Vector3 _offset;
    [Range(0, 0.99f)] [SerializeField] private float _distanceMultiplier;

    public event UnityAction<GameObject> WheatFind;

    private GameObject _scanResult = null;

    private void Update()
    {
        _scanResult = RayToScan();
        if (_scanResult != null)
        {
            WheatFind?.Invoke(_scanResult);
        }
    }

    private GameObject RayToScan()
    {
        GameObject result = null;
        
        float rayStep = 0;
        float distanceMultiplier;
        for (int i = 0; i < _rays; i++)
        {
            var x = Mathf.Sin(rayStep);
            var y = Mathf.Cos(rayStep);
            distanceMultiplier = Mathf.Pow(_distanceMultiplier, i);
            
            rayStep += _angle * Mathf.Deg2Rad / _rays;

            Vector3 dir = transform.TransformDirection(new Vector3(x, 0, y));

            result = GetRaycast(dir, _maxDistance * distanceMultiplier);

            if (x != 0)
            {
                dir = transform.TransformDirection(new Vector3(-x, 0, y));
                result = GetRaycast(dir, _maxDistance * distanceMultiplier);
            }
        }

        return result;
    }

    private GameObject GetRaycast(Vector3 direction, float distance)
    {
        GameObject result = null;
        RaycastHit hit = new RaycastHit();
        Vector3 position = transform.position + _offset;

        if (Physics.Raycast(position, direction, out hit, distance))
        {
            Debug.Log("hit");
            if (hit.transform.TryGetComponent(out Wheat wheat))
            {
                Debug.Log("wheat hit");
                result = wheat.gameObject;
                Debug.DrawLine(position, hit.point, Color.green);
            }
            else
            {
                Debug.DrawLine(position, hit.point, Color.blue);
            }
        }
        else
        {
            Debug.DrawRay(position, direction * distance, Color.red);
        }
        return result;
    }
}
