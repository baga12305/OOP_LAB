using NUnit.Framework;

// Интерфейс для изоляции случайности (например, выбор необязательной цели)
public interface IRandomSource
{
    int Next(int maxExclusive);
}

// Ручной дублёр (Stub), возвращающий заранее зафиксированное число
public class FixedRandomSource : IRandomSource
{
    private readonly int _fixedValue;

    public FixedRandomSource(int fixedValue)
    {
        _fixedValue = fixedValue;
    }

    public int Next(int maxExclusive) => _fixedValue; // Всегда детерминированный результат
}

public class GoalDoublesTests
{
    [Test]
    public void Выбор_случайной_цели_с_ручным_дублёром_работает_стабильно()
    {
        // Подготовка: передаем дублёр, который всегда выдает индекс 0
        IRandomSource stubRandom = new FixedRandomSource(0);

        // Действие
        int chosenIndex = stubRandom.Next(5);

        // Проверка: результат предсказуем при каждом запуске теста
        Assert.AreEqual(0, chosenIndex);
    }
}