using UnityEngine;
using TMPro;

public class GameBehavior : MonoBehaviour
{
    // I could not figure out how to implement destruction and instantiation in the manager.
    // Also, I have no idea why the score goes up by 2 when the bricks are destroyed
    // except for in the far left column.
    public static GameBehavior Instance;
    [SerializeField] TMP_Text _scoreUI;
    
    private int _score;
    
    public int Score
    {
        get => _score;
        
        set
        {
            _score = value;
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("New instance initialized...");
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Duplicate instance found and deleted...");
        }
    }

    void Start()
    {
        ResetGame();
    }
    public void ResetGame()
    {
        Score = 0;
    }
    
    public void ScorePoint()
    {
        Score++;
        _scoreUI.SetText(Score.ToString());
    }
}
