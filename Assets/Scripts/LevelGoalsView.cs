using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelGoalsView : MonoBehaviour
{
    [SerializeField] private int maxGoals = 5;

    private LevelGoals _levelGoals;

    public float Progress => _levelGoals != null ? _levelGoals.GetCompletionPercentage() : 0f;

    private void Start()
    {
        _levelGoals = new LevelGoals(maxGoals);

        try
        {
            
            _levelGoals.AddGoal(new KillGoal("Уничтожить монстров", 3, true, new CurrencyReward(500)));
            _levelGoals.AddGoal(new CollectGoal("Собрать кристаллы", 5, true, new StarReward(5)));
            _levelGoals.AddGoal(new CollectGoal("Осмотреть сундук", 1, false, new NextLevelReward()));

            Debug.Log("Уровень и цели инициализированы для сцены.");
        }
        catch (Exception e)
        {
            Debug.LogError($"Ошибка при инициализации целей: {e.Message}");
        }
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Goal[] goals = _levelGoals.GetGoals();
            bool foundGoal = false;

            foreach (var goal in goals)
            {
                if (!goal.IsCompleted)
                {
                    foundGoal = true;
                    try
                    {
                        _levelGoals.ProgressGoal(goal.Description, 1);
                        Debug.Log($"Продлена цель: '{goal.Description}' ({goal.Progress.Current}/{goal.Progress.Target}). Прогресс уровня: {Progress:F1}%");

                        if (_levelGoals.IsLevelCompleted())
                        {
                            Debug.Log("Уровень успешно завершен! Все обязательные цели выполнены.");
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning($"Не удалось обновить цель: {e.Message}");
                    }

                    break;
                }
            }

            if (!foundGoal)
            {
                Debug.LogWarning("Ошибка: Нет доступных целей для выполнения! Все цели уже закрыты.");
            }
        }
    }
}