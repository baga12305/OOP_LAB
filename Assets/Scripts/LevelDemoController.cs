using UnityEngine;

public class LevelDemoController : MonoBehaviour
{
    private LevelGoals _levelGoals;
    private GoalFactory _factory;

    private void Start()
    {
        Debug.Log("      ДЕМОНСТРАЦИЯ ФАБРИКИ ЦЕЛЕЙ     ");

        // 1. Создаем фабрику и менеджер уровня
        _factory = new GoalFactory();
        _levelGoals = new LevelGoals(5);

        // 2. Регистрируем новую цель для защиты ("nodamage") прямо в фабрике
        _factory.RegisterGoal("nodamage", (desc, target, mandatory, reward) =>
            new NoDamageGoal(desc, target, mandatory, reward));

        // 3. Создаем цели через фабрику 
        // Обрати внимание: клиент (контроллер) просит фабрику создать цель по строковому ID,
        // не зная названий конкретных классов (KillGoal, CollectGoal и т.д.).
        Goal goal1 = _factory.CreateGoal("kill", "Уничтожить врагов в лагере", 3, true, new CurrencyReward(200));
        Goal goal2 = _factory.CreateGoal("collect", "Собрать редкие кристаллы", 5, true, new StarReward(3));
        Goal goal3 = _factory.CreateGoal("nodamage", "Пройти уровень без урона", 1, false, new NextLevelReward());

        // 4. Добавляем цели в менеджер уровня
        _levelGoals.AddGoal(goal1);
        _levelGoals.AddGoal(goal2);
        _levelGoals.AddGoal(goal3);

        Debug.Log($"[Успех] На уровень добавлено целей: {_levelGoals.Count}");
        Debug.Log($"Начальный прогресс уровня: {_levelGoals.GetCompletionPercentage():F1}%");

        // 5. Симулируем игровой процесс (выполняем первую цель)
        Debug.Log("\nСимуляция: игрок выполняет цель 'Уничтожить врагов в лагере'");
        _levelGoals.ProgressGoal("Уничтожить врагов в лагере", 3);

        Debug.Log($"Статус g1: {goal1}");
        Debug.Log($"Обновленный прогресс уровня: {_levelGoals.GetCompletionPercentage():F1}%");
    }
}