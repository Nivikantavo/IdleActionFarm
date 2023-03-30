using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackpackFullnesViewer : MonoBehaviour
{
    [SerializeField] private Backpack _backpack;
    [SerializeField] private float _sliderStep;
    [SerializeField] private float _timeStep;

    private Slider _slider;
    private Coroutine _settingValueCoroutine;


    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _backpack.Capacity;
        _slider.value = 0;
    }

    private void OnEnable()
    {
        _backpack.FullnessChanged += OnFullnessChanged;
    }

    private void OnDisable()
    {
        _backpack.FullnessChanged -= OnFullnessChanged;
    }

    private void OnFullnessChanged(int newValue)
    {
        if(_settingValueCoroutine != null)
        {
            StopCoroutine(_settingValueCoroutine);
        }
        _settingValueCoroutine = StartCoroutine(SetSliderValue(newValue));
    }

    private IEnumerator SetSliderValue(int newValue)
    {
        WaitForSeconds delay = new WaitForSeconds(_timeStep);
        float currentValue = _slider.value;

        for (float i = 0; i < 1 ; i += _sliderStep)
        {
            _slider.value = Mathf.Lerp(currentValue, newValue, i);

            yield return null;
        }
    }
}
