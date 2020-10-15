using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [SerializeField] private int currentScore;
    [SerializeField] private int highScore;
    [SerializeField] private int moves;

    public static PlayerStats instance;

    public int HighScore { get => highScore; set => highScore = value; }
    public int CurrentScore { get => currentScore; set => currentScore = value; }
    public int Moves { get => moves; set => moves = value; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        HighScore = PlayerPrefs.GetInt("highscore", 0);
    }

    public void AddScore(int pointsToAdd)
    {
        CurrentScore += pointsToAdd;
    }

    public void SetHighScore()
    {
        if (CurrentScore>HighScore)
        {
            HighScore = CurrentScore;
            PlayerPrefs.SetInt("highscore", CurrentScore);
        }
    }

}
