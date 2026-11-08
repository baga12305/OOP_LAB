using System;

public class NoDamageGoal : Goal
{
    public NoDamageGoal(string description, int targetValue, bool isMandatory, IGoalReward reward)
        : base(description, targetValue, isMandatory, reward) { }

    public override void UpdateProgress(int amount)
    {
        if (isCompleted) return;

        progress.Increment(amount);
        if (progress.IsComplete && !isCompleted)
        {
            Complete();
        }
    }
}