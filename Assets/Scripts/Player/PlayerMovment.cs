using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovment : MonoBehaviour
{
    public bool Moving { get; private set; }

    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;

    private Vector3 _input;
    private Vector3 _velocity;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        Moving = false;
    }

    private void Update()
    {
        _input = new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
        _velocity = _input.normalized * _speed;

        if (_input != Vector3.zero)
        {
            transform.forward = _input;
            Moving = true;
        }
        else
        {
            Moving = false;
        }
    }
    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _velocity * Time.deltaTime);
    }
}
