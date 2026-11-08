using System;
using UnityEngine;

public class LevelBuilderDemo : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("      ДЕМОНСТРАЦИЯ ПАТТЕРНА СТРОИТЕЛЬ       ");

        // Cборка валидного уровня
        try
        {
            Goal goal1 = new KillGoal("Убить 5 монстров", 5, true, new CurrencyReward(100));
            Goal goal2 = new CollectGoal("Собрать 2 ключа", 2, true, new StarReward(3));

            LevelConfig level1 = new LevelBuilder()
                .SetName("Замок Дракона")
                .AddGoal(goal1)
                .AddGoal(goal2)
                .SetTimeLimit(120f)
                .SetStarThreshold(3)
                .SetNextLevel("Level_02")
                .Build();

            Debug.Log($"[Успешно создан]: {level1}");
        }
        catch (Exception e)
        {
            Debug.LogError($"[Ошибка]: {e.Message}");
        }

        Debug.Log("\n   Проверка защиты: сборка уровня без целей    ");
        try
        {
            LevelConfig invalidLevel = new LevelBuilder()
                .SetName("Пустой уровень")
                .SetTimeLimit(60f)
                .Build(); // ОШИБКА
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[Защита отработала корректно]: {e.Message}");
        }
    }
}