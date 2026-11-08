using NUnit.Framework;
using UnityEngine;

public class LevelPrototypeTests
{
    [Test]
    public void Изменение_состояния_копии_не_влияет_на_исходный_образец()
    {
   
        var configAsset = ScriptableObject.CreateInstance<LevelConfigSO>();
        var state1 = configAsset.CreateRuntimeState();
        var state2 = configAsset.CreateRuntimeState();

        state1.TimeRemaining = 15f;
        state1.CurrentScore = 500;

        Assert.AreEqual(60f, configAsset.BaseTimeLimit);
        Assert.AreEqual(60f, state2.TimeRemaining);
        Assert.AreEqual(0, state2.CurrentScore);

        Object.DestroyImmediate(configAsset);
    }
}