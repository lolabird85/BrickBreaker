using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    [Header("Ball Properties")]
    [SerializeField] private float _launchForce = 5f;
    [SerializeField, Range(1.0f, 2.0f)] private float _speedIncrement = 1.1f;
    [SerializeField, Range(0.0f, 1.0f)] private float _paddleInfluence = 0.5f;
    [SerializeField, Range(0.0f, 1.0f)] private float _steepnessThreshold = 0.25f;
    
    private AudioSource _source;
    Rigidbody2D _rb;
    private Vector2 direction;
    
    [Header("Audio Properties")]
    [SerializeField] private AudioClip _wallHit;
    [SerializeField] private AudioClip _paddleHit;
    [SerializeField] private AudioClip _fall;
    [SerializeField] private AudioClip _brickBreak;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _source = GetComponent<AudioSource>();
        ResetBall();
    }

    void Update()
    {
        _rb.simulated = GameBehavior.Instance.State == Utilities.GameState.Play;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            if (!Mathf.Approximately(collision.rigidbody.linearVelocityX, 0.0f))
            {
                Debug.Log("Collision with paddle :D");
                direction = _rb.linearVelocity * (1 - _paddleInfluence)
                                    + collision.rigidbody.linearVelocity * _paddleInfluence;
                direction.Normalize();
                CheckSteepness(ref direction);
                _rb.linearVelocity = _rb.linearVelocity.magnitude * direction * _speedIncrement;
            }
            _source.PlayOneShot(_paddleHit);
        }
        else if (collision.gameObject.CompareTag("Brick"))
        {
            _source.PlayOneShot(_brickBreak);
        }
        else
        { 
            _source.pitch = Random.Range(0.7f, 1.1f);
            _source.volume = Random.Range(0.8f, 1.0f);
            
            _source.clip = _wallHit;
            _source.Play();
        }
        CheckSteepness(ref direction);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        ResetBall();
        _source.PlayOneShot(_fall);
        Destroy(gameObject, _fall.length);
    }
    
    private void CheckSteepness(ref Vector2 direction)
    {
        if (Mathf.Abs(direction.y) < _steepnessThreshold)
        {
            direction.y += 0.5f * Mathf.Sign(direction.y);
            direction.Normalize();
        }
    }
    
    private void ResetBall()
    {
        Vector2 direction = Random.onUnitCircle;
        CheckSteepness(ref direction);
        _rb.AddForce(direction * _launchForce, ForceMode2D.Impulse);
    }
}
