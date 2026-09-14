using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    public float Speed = 5.0f;
        
    public KeyCode LeftDirection = KeyCode.LeftArrow;
    public KeyCode RightDirection = KeyCode.RightArrow;
    
    void Update()
    {
        float movement = 0.0f;
        
        if (Input.GetKey(LeftDirection))
        {
            movement -= Speed;
        }

        if (Input.GetKey(RightDirection))
        {
            movement += Speed;
        }

        movement *= Time.deltaTime;
        transform.Translate(movement, 0.0f, 0.0f);
    }
}
