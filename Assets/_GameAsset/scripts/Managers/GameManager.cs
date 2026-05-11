using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance {get; private set;}
   public event Action<GameState> OnGameStateChanged;
   [SerializeField] private EggCounterUI _eggCounterUI;
   [SerializeField] private int _maxEggCount=5;
   private int _currentEggCount;
   private GameState _currentGameState;
    private void Awake()
    {
        Instance=this;
    }
    private void Onable()
    {
     ChangeGameState(GameState.Play);   
    }
    public void ChangeGameState(GameState gameState)
    {
        OnGameStateChanged?.Invoke(gameState);
        _currentGameState=gameState;
        Debug.Log("Game State="+gameState);
    }
    public void OnEggCollected()
    {
        _currentEggCount++;
        _eggCounterUI.SetEggCounterText(_currentEggCount,_maxEggCount);
        if (_currentEggCount == _maxEggCount)
        {
            Debug.Log("game win");
            _eggCounterUI.SetEggCompleted();
            ChangeGameState(GameState.GameOver);
        }
        Debug.Log("Egg Count="+_currentEggCount);
    }
    public GameState GetCurrentGameState()
    {
        return _currentGameState;
    }
}
