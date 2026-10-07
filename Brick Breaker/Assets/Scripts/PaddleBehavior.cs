using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    private float _direction;
    
    [SerializeField] float _speed = 5.0f;

    [SerializeField] KeyCode _leftDirection = KeyCode.LeftArrow;
    [SerializeField] KeyCode _rightDirection = KeyCode.RightArrow;
    
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocityX = _direction * _speed;
    }
    
    void Update()
    {
        _direction = 0.0f;

        if (GameBehavior.Instance.State == Utilities.GameState.Play)
        {
            if (Input.GetKey(_leftDirection))
            {
                _direction -= 1.0f;
            }

            if (Input.GetKey(_rightDirection))
            {
                _direction += 1.0f;
            }
        }
    }
}
