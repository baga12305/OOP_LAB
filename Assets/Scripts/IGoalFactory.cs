public interface IGoalFactory
{
    Goal CreateGoal(string id, string description, int targetValue, bool isMandatory, IGoalReward reward);
}