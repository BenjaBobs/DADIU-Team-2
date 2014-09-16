using UnityEngine;
using System.Collections;

public static class Player {
	static int lives = 3;
    private static int score;

    public static int Lives
    {
        get
        {
            return lives;
        }
        set
        {
			int dmg = value - lives;            
			if (dmg < 0) OnTakeDamage(dmg);
			else OnHeal(dmg*-1);

            if (lives > 0 && value <= 0)
            {
				OnGameOver ();
            }
			else if (lives > value && value > 0)
				Grid.WipeBoard();
			lives = value;
        }
    }

	public static int Score
	{
		get
		{
			return score;
		}
		set
		{
			int amount = value - score;
			score = value;
			if (amount > 0) OnScoreIncrease(amount);
		}
	}

	private static void OnGameOver()
	{
		AudioManager.PlayGameOver ();
		Settings.instance.SetPause(true);
		//TODO: Initialize game end
		QADebugging.staticRef.hasLost = true;
        guiScore.staticRef.enabled = true;
        
	}

	private static void OnTakeDamage(int dmg)
	{
		AudioManager.PlayLoseLife ();
	}
	private static void OnHeal(int heal_amount)
	{
	}

	private static void OnScoreIncrease(int amount)
	{
		//AudioManager.PlayPointCount ();
	}

    public static void Reset()
    {
        lives = 3;
        score = 0;
    }
}
