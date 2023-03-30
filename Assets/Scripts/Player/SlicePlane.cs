using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlicePlane : MonoBehaviour
{
    public event UnityAction<GameObject> WheatFounded;
    public event UnityAction<GameObject> WheatLost;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Wheat wheat))
        {
            WheatFounded?.Invoke(wheat.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Wheat wheat))
        {
            WheatLost?.Invoke(wheat.gameObject);
        }
    }
}
