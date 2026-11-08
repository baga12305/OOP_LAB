using System;

public interface ILevelGoalTracker
{
    bool ProgressGoal(string description, int amount = 1);
    bool IsLevelCompleted();
}