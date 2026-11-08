using System;
using NUnit.Framework;

public class LevelBuilderTests
{
    [Test]
    public void Сборка_уровня_с_корректными_параметрами_проходит_успешно()
    {
        var goal = new KillGoal("Убить 5 врагов", 5, true, new CurrencyReward(50));
        var level = new LevelBuilder()
            .SetName("Уровень 1")
            .AddGoal(goal)
            .SetTimeLimit(120f)
            .SetStarThreshold(3)
            .SetNextLevel("Level_2")
            .Build();

        Assert.AreEqual("Уровень 1", level.LevelName);
        Assert.AreEqual(1, level.Goals.Count);
        Assert.AreEqual(120f, level.TimeLimit);
    }

    [Test]
    public void Сборка_уровня_без_целей_выбрасывает_исключение()
    {
        var builder = new LevelBuilder().SetName("Пустой уровень");

        var ex = Assert.Throws<InvalidOperationException>(() => builder.Build());
        StringAssert.Contains("без целей не может быть создан", ex.Message);
    }

    [Test]
    public void Сборка_уровня_с_невалидным_временем_выбрасывает_исключение()
    {
        var goal = new KillGoal("Убить 1 врага", 1, true, new CurrencyReward(10));
        var builder = new LevelBuilder()
            .SetName("Тест")
            .AddGoal(goal)
            .SetTimeLimit(-50f);

        var ex = Assert.Throws<InvalidOperationException>(() => builder.Build());
        StringAssert.Contains("время на уровень должно быть больше 0", ex.Message);
    }
}