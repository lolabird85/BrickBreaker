using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    [SerializeField] private float _launchForce = 5f;
    [SerializeField] private float _speedIncrement = 1.1f;
    [SerializeField] private float _paddleInfluence = 0.5f;
    
    private AudioSource _source;
    [SerializeField] private AudioClip _wallHit;
    [SerializeField] private AudioClip _paddleHit;
    [SerializeField] private AudioClip _fall;
    [SerializeField] private AudioClip _brickBreak;
    
    Rigidbody2D _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _source = GetComponent<AudioSource>();
        ResetBall();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            if (!Mathf.Approximately(collision.rigidbody.linearVelocityX, 0.0f))
            {
                Debug.Log("Collision with paddle :D");
                Vector2 direction = _rb.linearVelocity * (1 - _paddleInfluence)
                                    + collision.rigidbody.linearVelocity * _paddleInfluence;
                _rb.linearVelocity = _rb.linearVelocity.magnitude * direction.normalized * _speedIncrement;
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
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        ResetBall();
        _source.PlayOneShot(_fall);
        GameBehavior.Instance.ResetGame();
    }
    
    private void ResetBall()
    {
        _rb.linearVelocity = Vector2.zero;
        transform.position = new Vector3(0.0f, -1.0f, 0.0f);
        Vector2 direction = Random.onUnitCircle;
        _rb.AddForce(direction * _launchForce, ForceMode2D.Impulse);
    }
}
