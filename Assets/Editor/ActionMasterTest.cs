using UnityEngine;
using UnityEditor;
using NUnit.Framework;

[TestFixture]
public class ActionMasterTest
{
    private ActionMaster actionMaster = new ActionMaster();
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;

    [Test]
    public void PassingTest()
    {
        Assert.IsTrue(true);
    }

    [Test]
    public void FirstStrikeReturnsEndTurn()
    {
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }
}
