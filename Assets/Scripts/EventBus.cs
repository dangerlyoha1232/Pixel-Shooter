using System;

public static class EventBus
{
    public static event Action<float> OnEnemyDie;

    public static void SendEnemyDie(float score)
    {
        OnEnemyDie?.Invoke(score);
    }
}