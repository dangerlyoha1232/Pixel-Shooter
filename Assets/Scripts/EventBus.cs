using System;

public static class EventBus
{
    public static event Action<float> OnEnemyDie;
    public static event Action OnPlayerDie;
    
    public static void SendEnemyDie(float score)
    {
        OnEnemyDie?.Invoke(score);
    }

    public static void SendPlayerDie()
    {
        OnPlayerDie?.Invoke();
    }
}