using System;
using UnityEngine;

public class Demo : MonoBehaviour
{
    private void Start()
    {

        LevelGoals level = new LevelGoals(3);

        // Создаем цели-наследники с полиморфными наградами (композиция интерфейсов)
        Goal g1 = new KillGoal("Уничтожить врагов", 3, true, new CurrencyReward(150));
        Goal g2 = new CollectGoal("Собрать артефакты", 2, true, new StarReward(3));
        Goal g3 = new CollectGoal("Найти тайник", 1, false, new NextLevelReward());

        level.AddGoal(g1);
        level.AddGoal(g2);
        level.AddGoal(g3);

        Debug.Log($"Создано целей за сессию (static): {Goal.CreatedCount}");
        Debug.Log($"Начальный прогресс: {level.GetCompletionPercentage():F1}%");

        // Частично продвигаем первую цель
        Debug.Log("\n    Продвигаем цель 'Уничтожить врагов' на 2 из 3");
        level.ProgressGoal("Уничтожить врагов", 2);
        Debug.Log($"Состояние g1: {g1}");

        
        Debug.Log("\n    Добиваем цель до конца");
        level.ProgressGoal("Уничтожить врагов", 1);
        Debug.Log($"Состояние g1 после завершения: {g1}");
        Debug.Log($"Прогресс уровня: {level.GetCompletionPercentage():F1}%");

        Debug.Log("\n    Проверка защиты try-catch  ");
        try
        {
            g1.Complete(); 
        }
        catch (Exception e)
        {
            Debug.Log($"[Ошибка отбита]: {e.Message}");
        }
    }
}