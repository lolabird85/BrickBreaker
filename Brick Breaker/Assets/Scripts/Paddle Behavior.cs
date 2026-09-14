using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    public float Speed = 5.0f;
        
    public KeyCode LeftDirection = KeyCode.LeftArrow;
    public KeyCode RightDirection = KeyCode.RightArrow;
    public Transform PaddlePosition;

    void Start()
    {
        PaddlePosition = GameObject.Find("Paddle").GetComponent<Transform>();
    }
    
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
        
        //I tried to set a boundary for the level using transform
        //This is my progress so far
        //I wanted to do an if statement that if the paddle reached a certain position, I'd stop it from moving farther
        //I couldn't get it to work without making the paddle unable to move completely
        if (PaddlePosition.position.x >= 5.0f)
        {
            if (Input.GetKey(RightDirection))
            {
            }
        }
        else if (PaddlePosition.position.x <= -5.0f)
        {
            if (Input.GetKey(LeftDirection))
            {
            }
        }
    }
}
