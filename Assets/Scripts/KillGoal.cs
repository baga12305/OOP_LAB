using System;

public class KillGoal : Goal
{
    public KillGoal(string description, int targetCount, bool isMandatory, IGoalReward reward)
        : base(description, targetCount, isMandatory, reward) { }
    public override void UpdateProgress(int amount)
    {
        if (isCompleted) return;

        progress.Increment(amount); // Увеличиваем прогресс через композицию
        if (progress.IsComplete && !isCompleted)
        {
            Complete(); 
        }
    }
}