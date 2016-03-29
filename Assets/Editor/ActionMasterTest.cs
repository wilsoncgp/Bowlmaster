using UnityEngine;
using UnityEditor;
using NUnit.Framework;

[TestFixture]
public class ActionMasterTest
{
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;

    private ActionMaster actionMaster;

    [SetUp]
    public void Setup()
    {
        actionMaster = new ActionMaster();
    }

    [Test]
    public void FirstStrikeReturnsEndTurn()
    {
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void BowlEightReturnsTidy()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
    }

    [Test]
    public void BowlSpareReturnsEndTurn()
    {
        int firstBowl = Random.Range(0, 10);

        Assert.AreEqual(tidy, actionMaster.Bowl(firstBowl));

        int secondBowl = 10 - firstBowl;

        Assert.AreEqual(endTurn, actionMaster.Bowl(secondBowl));
    }

    [Test]
    public void BowlSpareWhenMissFirstBowlReturnsEndTurn()
    {
        actionMaster.Bowl(0);
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void BowlSpareWhenMissFirstBowlThenBowlLessThanTenReturnsTidy()
    {
        actionMaster.Bowl(0);
        actionMaster.Bowl(10);
        Assert.AreEqual(tidy, actionMaster.Bowl(3));
    }

    [Test]
    public void BowlTwiceNoStrikeNoSpareReturnsEndTurn()
    {
        int firstBowl = Random.Range(0, 9);

        Assert.AreEqual(tidy, actionMaster.Bowl(firstBowl));

        int secondBowl = 9 - firstBowl;

        Assert.AreEqual(endTurn, actionMaster.Bowl(secondBowl));
    }

    [Test]
    public void BowlThreeInFirstBowlOfLastFrameReturnsTidy()
    {
        BowlToLastFrame(ref actionMaster);

        Assert.AreEqual(tidy, actionMaster.Bowl(3));
    }

    [Test]
    public void BowlThreeAndSevenInFirstAndSecondBowlsOfLastFrameReturnsReset()
    {
        BowlToLastFrame(ref actionMaster);

        actionMaster.Bowl(3);
        Assert.AreEqual(reset, actionMaster.Bowl(7));
    }

    [Test]
    public void BowlStrikeInFirstBowlOfLastFrameReturnsReset()
    {
        BowlToLastFrame(ref actionMaster);

        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void BowlStrikeInFirstAndSecondBowlOfLastFrameReturnsReset()
    {
        BowlToLastFrame(ref actionMaster);

        actionMaster.Bowl(10);
        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void BowlStrikeInAllThreeBowlsOfLastFrameReturnsEndGame()
    {
        BowlToLastFrame(ref actionMaster);

        actionMaster.Bowl(10);
        actionMaster.Bowl(10);
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }

    [Test]
    public void BowlTwelveStrikesReturnsEndGame()
    {
        for(int i = 0; i < 11; i++)
        {
            actionMaster.Bowl(10);
        }

        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }

    [Test]
    public void BowlStrikeThenZeroInLastFrameReturnsTidy()
    {
        BowlToLastFrame(ref actionMaster);

        actionMaster.Bowl(10);
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
    }

    [Test]
    public void BowlStrikeThenSpareInLastFrameReturnsEndGame()
    {
        BowlToLastFrame(ref actionMaster);

        actionMaster.Bowl(10);
        actionMaster.Bowl(3);
        Assert.AreEqual(endGame, actionMaster.Bowl(7));
    }

    [Test]
    public void BowlTwentyTimesWithNoStrikesOrSparesReturnsEndGame()
    {
        for(int i = 0; i < 19; i++)
        {
            actionMaster.Bowl(1);
        }

        Assert.AreEqual(endGame, actionMaster.Bowl(1));
    }

    [Test]
    public void BowlFullGameThenBowlThreeReturnsTidy()
    {
        BowlFullGame(ref actionMaster);

        Assert.AreEqual(tidy, actionMaster.Bowl(3));
    }

    [Test]
    public void BowlFullGameThenBowlSpareReturnsEndTurn()
    {
        BowlFullGame(ref actionMaster);

        actionMaster.Bowl(5);
        Assert.AreEqual(endTurn, actionMaster.Bowl(5));
    }

    [Test]
    public void BowlFullGameThenBowlStrikeReturnsEndTurn()
    {
        BowlFullGame(ref actionMaster);

        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void BowlFullGameThenBowlStrikeInLastFrameReturnsReset()
    {
        BowlFullGame(ref actionMaster);
        BowlToLastFrame(ref actionMaster);

        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void BowlFullGameThenBowlSpareInLastFrameReturnsReset()
    {
        BowlFullGame(ref actionMaster);
        BowlToLastFrame(ref actionMaster);

        actionMaster.Bowl(3);
        Assert.AreEqual(reset, actionMaster.Bowl(7));
    }

    [Test]
    public void BowlFullGameThenBowlThreeStrikesInLastFrameReturnsEndGame()
    {
        BowlFullGame(ref actionMaster);
        BowlToLastFrame(ref actionMaster);

        actionMaster.Bowl(10);
        actionMaster.Bowl(10);
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }

    private void BowlToLastFrame(ref ActionMaster aMaster)
    {
        for(int i = 0; i < 9; i++)
        {
            aMaster.Bowl(10);
        }
    }

    private void BowlFullGame(ref ActionMaster aMaster)
    {
        for(int i = 0; i < 12; i++)
        {
            aMaster.Bowl(10);
        }
    }
}
