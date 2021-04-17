using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillGoal : Goal
{
    public int EnmeyID { get; set; }

    public KillGoal(int enemyID, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.EnmeyID = enemyID;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    }

    public override void Init()
    {
        base.Init();
    }

    //void EnemeyDied(IEnemy enemy)
    //{
    //    if (enemy.ID == this.EnemyID)
    //    {

    //    }
    //}

    //public int enemyID;

    //public KillGoal(int amountNeeded, int enemyID, Quest quest)
    //{
    //    countCurrent = 0;
    //    countNeeded = amountNeeded;
    //    completed = false;
    //    this.enemyID = enemyID;
    //}

    //void EnemyKilled(int enemyID)
    //{
    //    if (this.enemyID == enemyID)
    //    {
    //        Increment(1);
    //    }
    //}
}
