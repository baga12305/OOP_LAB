using System;
using NUnit.Framework;

public class GoalTests
{
    // Заглушка (Stub) награды для изоляции тестов целей
    private class DummyReward : IGoalReward
    {
        public void ApplyReward() { }
    }

    [Test]
    public void Описание_цели_не_может_быть_пустым()
    {
        // Подготовка и действие + Проверка: пустая строка или пробелы выбрасывают ArgumentException
        Assert.Throws<ArgumentException>(() => new KillGoal("", 5, true, new DummyReward()));
        Assert.Throws<ArgumentException>(() => new KillGoal("   ", 5, true, new DummyReward()));
    }

    [TestCase(-1)]
    [TestCase(-100)]
    public void Награда_не_может_быть_отрицательной(int invalidAmount)
    {
        // Проверка границ для отрицательных значений наград через TestCase
        Assert.Throws<ArgumentException>(() => new CurrencyReward(invalidAmount));
    }

    [Test]
    public void Выполненную_цель_нельзя_выполнить_повторно()
    {
        // Подготовка
        var goal = new KillGoal("Уничтожить врагов", 1, true, new DummyReward());
        goal.UpdateProgress(1); // Достигаем цели

        Assert.IsTrue(goal.IsCompleted, "Цель должна перейти в статус выполненных.");

        // Действие и Проверка: повторный вызов Complete вызывает InvalidOperationException
        Assert.Throws<InvalidOperationException>(() => goal.Complete());
    }

    [Test]
    public void Выполненную_цель_нельзя_отменить()
    {
        // Подготовка
        var goal = new KillGoal("Уничтожить врагов", 1, true, new DummyReward());
        goal.UpdateProgress(1);

        // Действие и Проверка: отмена выполнения запрещена для закрытых целей
        Assert.Throws<InvalidOperationException>(() => goal.CancelCompletion());
    }
}