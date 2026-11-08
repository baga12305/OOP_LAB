using System;
using UnityEngine;

public interface IGoalReward
{
    void ApplyReward();
}

public class CurrencyReward : IGoalReward
{
    private readonly int _coins;

    public CurrencyReward(int coins)
    {
        if (coins < 0)
            throw new ArgumentException("Количество монет не может быть отрицательным.", nameof(coins));

        _coins = coins;
    }

    public void ApplyReward()
    {
        Debug.Log($"[Награда]: Выдано монет: {_coins}");
    }
}

public class StarReward : IGoalReward
{
    private readonly int _starsCount;

    public StarReward(int starsCount)
    {
        _starsCount = starsCount;
    }

    public void ApplyReward()
    {
        Debug.Log($"[Награда]: Получено звезд: {_starsCount}");
    }
}

public class NextLevelReward : IGoalReward
{
    public void ApplyReward()
    {
        Debug.Log("[Награда]: Открыт доступ к следующему уровню!");
    }
}