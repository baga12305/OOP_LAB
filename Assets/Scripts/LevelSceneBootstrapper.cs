using UnityEngine;

public class LevelSceneBootstrapper : MonoBehaviour
{
    [SerializeField] private GoalTrigger goalTriggerOnScene;

    private LevelGoals _levelGoals;

    private void Awake()
    {
        // 1. Создаем чистую бизнес-логику уровня
        _levelGoals = new LevelGoals(5);

        // Добавляем стартовую цель
        _levelGoals.AddGoal(new KillGoal("Уничтожить монстров", 2, true, new CurrencyReward(150)));

        // 2. Передаем зависимость объекту на сцене через метод Init
        if (goalTriggerOnScene != null)
        {
            goalTriggerOnScene.Init(_levelGoals);
            Debug.Log("LevelSceneBootstrapper успешно инициализировал сцену.");
        }
        else
        {
            Debug.LogError("Не назначен GoalTrigger в инспекторе сборщика сцены!");
        }
    }
}