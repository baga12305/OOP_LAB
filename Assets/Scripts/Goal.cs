using System;

public abstract class Goal
{
    private static int createdCount = 0;
    public const int MAX_GOALS = 5;

    public static int CreatedCount => createdCount;

    protected readonly string description;
    protected readonly bool isMandatory;
    protected readonly Progress progress;   
    protected readonly IGoalReward reward;  
    protected bool isCompleted;

    public string Description => description;
    public bool IsMandatory => isMandatory;
    public bool IsCompleted => isCompleted;
    public Progress Progress => progress;

    protected Goal(string description, int targetValue, bool isMandatory, IGoalReward reward)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Описание не может быть пустым.");
        if (reward == null)
            throw new ArgumentNullException(nameof(reward));

        this.description = description;
        this.isMandatory = isMandatory;
        this.progress = new Progress(0, targetValue);
        this.reward = reward;
        this.isCompleted = false;

        createdCount++;
    }

    public virtual void Complete()
    {
        if (isCompleted)
            throw new InvalidOperationException("Цель уже выполнена.");

        isCompleted = true;
        reward.ApplyReward(); 
    }

    public virtual void CancelCompletion()
    {
        throw new InvalidOperationException("Выполненную цель нельзя отменить.");
    }

    public abstract void UpdateProgress(int amount);

    public override string ToString()
    {
        string status = isCompleted ? "выполнено" : $"в процессе ({progress.Current}/{progress.Target})";
        string type = isMandatory ? "обязательная" : "второстепенная";
        return $"{description} [{type}] — {status}";
    }
}