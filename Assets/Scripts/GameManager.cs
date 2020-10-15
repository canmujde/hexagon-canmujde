using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private GameState state;
    private PlayingState p_State;

    public static GameManager instance;

    public GameState State { get => state; set => state = value; }
    public PlayingState P_State { get => p_State; set => p_State = value; }

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
        SetState(0);
        Debug.Log("Game Initialized. State is: " + State.ToString());
    }

    public void SetState(int index)
    {
        State = (GameState)index;
    }



}

public enum GameState { Prepare, Playing, GameOver }
public enum PlayingState { Select, Rotate }