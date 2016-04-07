using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class ActionMasterTest
{
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    
    [Test]
    public void FirstStrikeReturnsEndTurn()
    {
        Assert.AreEqual(endTurn, ActionMaster.NextAction(new List<int>() { 10 }));
    }

    [Test]
    public void BowlEightReturnsTidy()
    {
        Assert.AreEqual(tidy, ActionMaster.NextAction(new List<int>() { 8 }));
    }

    [Test]
    public void BowlStrikeFirstReturnsEndTurn()
    {
        Assert.AreEqual(endTurn, ActionMaster.NextAction(new List<int>() { 10 }));
    }

    [Test]
    public void BowlSpareReturnsEndTurn()
    {
        int firstBowl = Random.Range(0, 10);
        int secondBowl = 10 - firstBowl;

        Assert.AreEqual(endTurn, ActionMaster.NextAction(new List<int>() { firstBowl, secondBowl }));
    }

    [Test]
    public void BowlSpareWhenMissFirstBowlReturnsEndTurn()
    {
        Assert.AreEqual(endTurn, ActionMaster.NextAction(new List<int>() { 0, 10 }));
    }

    [Test]
    public void BowlSpareWhenMissFirstBowlThenBowlLessThanTenReturnsTidy()
    {
        Assert.AreEqual(tidy, ActionMaster.NextAction(new List<int>() { 0, 10, 3 }));
    }

    [Test]
    public void BowlTwiceNoStrikeNoSpareReturnsEndTurn()
    {
        int firstBowl = Random.Range(0, 9);
        int secondBowl = 9 - firstBowl;

        Assert.AreEqual(endTurn, ActionMaster.NextAction(new List<int>() { firstBowl, secondBowl }));
    }

    [Test]
    public void BowlThreeInFirstBowlOfLastFrameReturnsTidy()
    {
        Assert.AreEqual(tidy, ActionMaster.NextAction(new List<int>() { 10, 10, 10, 10, 10, 10, 10, 10, 10, 3 }));
    }

    [Test]
    public void BowlThreeAndSevenInFirstAndSecondBowlsOfLastFrameReturnsReset()
    {
        Assert.AreEqual(reset, ActionMaster.NextAction(new List<int>() { 10, 10, 10, 10, 10, 10, 10, 10, 10, 3, 7 }));
    }

    [Test]
    public void BowlStrikeInFirstBowlOfLastFrameReturnsReset()
    {
        Assert.AreEqual(reset, ActionMaster.NextAction(new List<int>() { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }));
    }

    [Test]
    public void BowlStrikeInFirstAndSecondBowlOfLastFrameReturnsReset()
    {
        Assert.AreEqual(reset, ActionMaster.NextAction(new List<int>() { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }));
    }

    [Test]
    public void BowlStrikeInAllThreeBowlsOfLastFrameReturnsEndGame()
    {
        Assert.AreEqual(endGame, ActionMaster.NextAction(new List<int>() { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 10, 10 }));
    }

    [Test]
    public void BowlTwelveStrikesReturnsEndGame()
    {
        Assert.AreEqual(endGame, ActionMaster.NextAction(new List<int>() { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }));
    }

    [Test]
    public void BowlStrikeThenZeroInLastFrameReturnsTidy()
    {
        Assert.AreEqual(tidy, ActionMaster.NextAction(new List<int>() { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 0 }));
    }

    [Test]
    public void BowlStrikeThenSpareInLastFrameReturnsEndGame()
    {
        Assert.AreEqual(endGame, ActionMaster.NextAction(new List<int>() { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 0, 10 }));
    }

    [Test]
    public void BowlTwentyTimesWithNoStrikesOrSparesReturnsEndGame()
    {
        Assert.AreEqual(endGame, ActionMaster.NextAction(new List<int>() { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }));
    }
}
