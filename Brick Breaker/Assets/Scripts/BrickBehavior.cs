using System;
using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    public Color[] Colors = new Color[4];
    private int _hitNumber;
    
    SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = Colors[0];
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (_hitNumber <= 1)
            {
                _hitNumber++;
                _spriteRenderer.color = Colors[_hitNumber];
            }
            else
            {
                Debug.Log("Got hit!");
                Destroy(gameObject, 0.09f);
                GameBehavior.Instance.ScorePoint();
            }
        } 
    }
}
