using System;

public class Progress
{
    private int current;
    private readonly int target;

    public int Current => current;
    public int Target => target;
    public bool IsComplete => current >= target;

    public Progress(int current, int target)
    {
        if (target <= 0)
            throw new ArgumentOutOfRangeException(nameof(target), "Значение должно быть больше 0");
        if (current < 0 || current > target)
            throw new ArgumentOutOfRangeException(nameof(current), "Вне допустимого диапазона");

        this.current = current;
        this.target = target;
    }

    public void Increment(int amount = 1)
    {
        if (amount <= 0)
            throw new ArgumentException("Шаг должен быть больше 0");

        current = Math.Min(target, current + amount);
    }
}