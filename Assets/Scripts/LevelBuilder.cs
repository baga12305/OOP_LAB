using System;
using System.Collections.Generic;

// Класс создаваемой конфигурации уровня
public class LevelConfig
{
    public string LevelName { get; }
    public List<Goal> Goals { get; }
    public float TimeLimit { get; }
    public int StarThreshold { get; }
    public string NextLevelName { get; }

    public LevelConfig(string levelName, List<Goal> goals, float timeLimit, int starThreshold, string nextLevelName)
    {
        LevelName = levelName;
        Goals = goals;
        TimeLimit = timeLimit;
        StarThreshold = starThreshold;
        NextLevelName = nextLevelName;
    }

    public override string ToString()
    {
        return $"Уровень: '{LevelName}', Целей: {Goals.Count}, Время: {TimeLimit}с, Звёзд: {StarThreshold}, Следующий: '{NextLevelName}'";
    }
}

// Builder
public class LevelBuilder
{
    private string _levelName;
    private readonly List<Goal> _goals = new List<Goal>();
    private float _timeLimit = 60f;
    private int _starThreshold = 3;
    private string _nextLevelName;

    // Название уровня 
    public LevelBuilder SetName(string name)
    {
        _levelName = name;
        return this;
    }

    // Набор целей 
    public LevelBuilder AddGoal(Goal goal)
    {
        if (goal != null)
        {
            _goals.Add(goal);
        }
        return this;
    }

    // Время на уровень
    public LevelBuilder SetTimeLimit(float timeLimit)
    {
        _timeLimit = timeLimit;
        return this;
    }

    // Порог для звёзд
    public LevelBuilder SetStarThreshold(int stars)
    {
        _starThreshold = stars;
        return this;
    }

    // Имя некст уровня
    public LevelBuilder SetNextLevel(string nextLevelName)
    {
        _nextLevelName = nextLevelName;
        return this;
    }

    // Сборка с проверкой обязательных параметров
    public LevelConfig Build()
    {
        if (string.IsNullOrWhiteSpace(_levelName))
        {
            throw new InvalidOperationException("Ошибка сборки: не указано название уровня!");
        }

        if (_goals.Count == 0)
        {
            throw new InvalidOperationException("Ошибка сборки: уровень без целей не может быть создан!");
        }

        if (_timeLimit <= 0)
        {
            throw new InvalidOperationException("Ошибка сборки: время на уровень должно быть больше 0!");
        }

        return new LevelConfig(_levelName, new List<Goal>(_goals), _timeLimit, _starThreshold, _nextLevelName);
    }
}