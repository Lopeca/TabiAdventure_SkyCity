using UnityEngine;

public enum GameState
{
    Playing
}
public class GameManager : Singleton<GameManager>
{
    [field:SerializeField] public GameState GameState { get; private set; }
    
    
    
    protected override void Awake()
    {
        base.Awake();
        GameState = GameState.Playing;
    }
}
