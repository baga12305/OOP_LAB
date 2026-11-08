using System;
using System.Collections.Generic;

public class GoalFactory : IGoalFactory
{
    // Словарь (реестр) для хранения создателей целей по строковому ключу
    private readonly Dictionary<string, Func<string, int, bool, IGoalReward, Goal>> _creators
        = new Dictionary<string, Func<string, int, bool, IGoalReward, Goal>>(StringComparer.OrdinalIgnoreCase);

    public GoalFactory()
    {
        // Регистрируем базовые типы целей
        RegisterGoal("kill", (desc, target, mandatory, reward) => new KillGoal(desc, target, mandatory, reward));
        RegisterGoal("collect", (desc, target, mandatory, reward) => new CollectGoal(desc, target, mandatory, reward));
    }

    // Метод регистрации новых типов
    public void RegisterGoal(string id, Func<string, int, bool, IGoalReward, Goal> creator)
    {
        _creators[id] = creator;
    }

    public Goal CreateGoal(string id, string description, int targetValue, bool isMandatory, IGoalReward reward)
    {
        if (!_creators.TryGetValue(id, out var creator))
        {
            throw new ArgumentException($"Неизвестная разновидность цели: '{id}'.", nameof(id));
        }

        return creator(description, targetValue, isMandatory, reward);
    }
}