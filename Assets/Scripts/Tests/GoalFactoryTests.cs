using System;
using NUnit.Framework;

public class GoalFactoryTests
{
    [Test]
    public void Фабрика_выбрасывает_исключение_при_запросе_неизвестного_идентификатора()
    {
        IGoalFactory factory = new GoalFactory();

        var ex = Assert.Throws<ArgumentException>(() =>
        {
            factory.CreateGoal("bad_id_test", "Тест", 1, true, new CurrencyReward(10));
        });

        StringAssert.Contains("Неизвестная разновидность цели", ex.Message);
    }

    [Test]
    public void Фабрика_успешно_создает_зарегистрированный_тип_цели()
    {
        var factory = new GoalFactory();

        // Регистрируем новую цель "nodamage" прямо в тесте
        factory.RegisterGoal("nodamage", (desc, target, mandatory, reward) => new NoDamageGoal(desc, target, mandatory, reward));

        var goal = factory.CreateGoal("nodamage", "Пройти без урона", 1, true, new CurrencyReward(50));

        Assert.IsNotNull(goal);
        Assert.IsInstanceOf<NoDamageGoal>(goal);
        Assert.AreEqual("Пройти без урона", goal.Description);
    }
}