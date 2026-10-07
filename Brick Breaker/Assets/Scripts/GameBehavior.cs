using UnityEngine;
using TMPro;

public class GameBehavior : MonoBehaviour
{
    public static GameBehavior Instance;
    
    private Utilities.GameState _state;
    public Utilities.GameState State
    {
        get => _state;

        set
        {
            _state = value;
            _pauseUI.enabled = State == Utilities.GameState.Pause;
        }
    }

    [SerializeField] private TMP_Text _pauseUI;
    [SerializeField] TMP_Text _scoreUI;
    
    private GameObject _ball;
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Transform _ballParent;

    private GameObject _brick;
    [SerializeField] private GameObject _brickPrefab;
    [SerializeField] private Transform _brickParent;
    [SerializeField] private AudioClip _fall;
    
    private int _score;
    public int Score
    {
        get => _score;
        
        set
        {
            _score = value;
            _scoreUI.SetText(Score.ToString());
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
        State = Utilities.GameState.Play;
    }
    public void ResetGame()
    {
        Score = 0;
    }
    
    public void ScorePoint()
    {
        Score++;
    }
    
    void Update()
    {
        // State machine transition
        if (Input.GetKeyDown(KeyCode.Space))
        {
            State = State == Utilities.GameState.Play ?
                Utilities.GameState.Pause :
                Utilities.GameState.Play;
        }
        if (!_ball)
        {
            ResetPoint();
            ResetGame();
        }
    }
    public void ResetPoint()
    {
        _ball = Instantiate(_ballPrefab, new Vector3(0.0f, -0.6f, 0.0f), Quaternion.identity, _ballParent);
        _brick = Instantiate(_brickPrefab, new Vector3(0.0f, 0.5f, 0.0f), Quaternion.identity, _brickParent);
    }
}
