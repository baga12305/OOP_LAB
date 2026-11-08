using System;
public class CollectGoal : Goal
{
    public CollectGoal(string description, int targetCount, bool isMandatory, IGoalReward reward)
        : base(description, targetCount, isMandatory, reward) { }

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