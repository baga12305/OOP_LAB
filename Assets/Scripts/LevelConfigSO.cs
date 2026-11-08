using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelConfig", menuName = "Configs/Level Config SO")]
public class LevelConfigSO : ScriptableObject
{
    [SerializeField] private string levelName = "Уровень 1";
    [SerializeField] private float baseTimeLimit = 60f;
    [SerializeField] private int starThreshold = 3;

    public string LevelName => levelName;
    public float BaseTimeLimit => baseTimeLimit;
    public int StarThreshold => starThreshold;

    // Метод создания копии для изменяемого во время игры состояния
    public LevelRuntimeState CreateRuntimeState()
    {
        return new LevelRuntimeState(this);
    }
}

// Класс-копия для динамических данных уровня
public class LevelRuntimeState
{
    public string LevelName { get; }
    public float TimeRemaining { get; set; }
    public int CurrentScore { get; set; }

    public LevelRuntimeState(LevelConfigSO config)
    {
        LevelName = config.LevelName;
        TimeRemaining = config.BaseTimeLimit;
        CurrentScore = 0;
    }
}