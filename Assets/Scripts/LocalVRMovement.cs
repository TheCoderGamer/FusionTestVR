using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class LocalVRMovement : MonoBehaviour
{
    
    [SerializeField] private InputActionReference movementInput;
    [SerializeField] private InputActionReference rotationInput;
    [SerializeField] private Transform head;
    public float movementSpeed = 5f;
    public float rotationSpeed = 5f;
    public float gravity = -9.8f;
    
    private CharacterController _characterController;
    
    private Vector2 _movement;
    private Vector2 _rotation;
    private Vector3 _direction;
    private float _yVelocity = 0f;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        Rotate();
        Gravity();
    }

    private void Move()
    {
        _movement = movementInput.action.ReadValue<Vector2>();
        _direction = new Vector3(_movement.x, 0, _movement.y);
        _direction = head.TransformDirection(_direction);
        _direction = Vector3.Scale(_direction, new Vector3(1, 0, 1)).normalized;
        _characterController.Move(_direction * (movementSpeed * Time.deltaTime));
    }

    private void Rotate()
    {
        _rotation = rotationInput.action.ReadValue<Vector2>();
        transform.rotation *= Quaternion.Euler(0, _rotation.x * rotationSpeed, 0);
    }
    
    private void Gravity()
    {
        if (_characterController.isGrounded)
        {
            _yVelocity = 0;
        }
        else
        {
            _yVelocity += gravity * Time.deltaTime; 
            _characterController.Move(new Vector3(0, _yVelocity, 0));
        }
    }


    private void OnEnable()
    {
        movementInput.action.Enable();
        rotationInput.action.Enable();
    }
    
    private void OnDisable()
    {
        movementInput.action.Disable();
        rotationInput.action.Disable();
    }
}
