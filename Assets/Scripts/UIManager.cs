using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Text highScore;
    [SerializeField] private Text currentScore;
    [SerializeField] private Text moves;

    [SerializeField] private Page[] pages;

    private void Start()
    {
        OnPrepareState();
        UpdateBestScore();
        UpdateCurrentScore();
        UpdateMoves();
    }

    public void OnPrepareState()
    {
        pages[0].Open();
        pages[1].Close();
        pages[2].Close();
    }

    public void OnPlayingState()
    {
        pages[0].Close();
        pages[1].Open();
        pages[2].Close();
    }

    public void OnGameOverState()
    {
        pages[0].Close();
        pages[1].Close();
        pages[2].Open();
    }

    public void UpdateBestScore()
    {
        highScore.text = "Best: " + PlayerStats.instance.HighScore;
    }
    public void UpdateCurrentScore()
    {
        currentScore.text = PlayerStats.instance.CurrentScore.ToString();
    }
    public void UpdateMoves()
    {
        moves.text = PlayerStats.instance.Moves.ToString();
    }

}
