using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Got hit!");
            Destroy(gameObject, 0.09f);
            GameBehavior.Instance.ScorePoint();
        } 
    }
}
