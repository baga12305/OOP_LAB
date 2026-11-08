using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private ILevelGoalTracker _goalTracker;

    // Внедрение зависимости через метод Init (без использования Singleton)
    public void Init(ILevelGoalTracker goalTracker)
    {
        _goalTracker = goalTracker;
    }

    public void InteractWithGoal(string goalDescription)
    {
        if (_goalTracker == null)
        {
            Debug.LogWarning("GoalTrigger не инициализирован: отсутствует ссылка на трекер целей!");
            return;
        }

        bool success = _goalTracker.ProgressGoal(goalDescription, 1);
        if (success)
        {
            Debug.Log($"Триггер успешно засчитал прогресс для цели: '{goalDescription}'");
        }
        else
        {
            Debug.Log($"Цель '{goalDescription}' уже выполнена или не найдена.");
        }
    }
}