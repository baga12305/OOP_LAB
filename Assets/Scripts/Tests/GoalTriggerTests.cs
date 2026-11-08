using NUnit.Framework;
using UnityEngine;

public class GoalTriggerTests
{
    private class FakeGoalTracker : ILevelGoalTracker
    {
        public bool WasCalled { get; private set; }
        public string LastDescription { get; private set; }
        public int LastAmount { get; private set; }

        public bool ProgressGoal(string description, int amount = 1)
        {
            WasCalled = true;
            LastDescription = description;
            LastAmount = amount;
            return true;
        }

        public bool IsLevelCompleted() => false;
    }

    [Test]
    public void Взаимодействие_с_целью_вызывает_обновление_прогресса_у_внедренного_трекера()
    {
        // Arrange
        var gameObject = new GameObject();
        var trigger = gameObject.AddComponent<GoalTrigger>();
        var fakeTracker = new FakeGoalTracker();

        trigger.Init(fakeTracker);

        // Act
        trigger.InteractWithGoal("Уничтожить монстров");

        // Assert
        Assert.IsTrue(fakeTracker.WasCalled, "Метод ProgressGoal должен был вызваться у трекера.");
        Assert.AreEqual("Уничтожить монстров", fakeTracker.LastDescription);
        Assert.AreEqual(1, fakeTracker.LastAmount);

        // Cleanup
        Object.DestroyImmediate(gameObject);
    }
}