using UnityEngine;
using System.Collections;

public static class Player {
	static int lives = 3;
    public static int score;

    public static int Lives
    {
        get
        {
            return lives;
        }
        set
        {
			int dmg = value - lives;
            lives = value;
			if (dmg > 0) OnTakeDamage(dmg);
			else OnHeal(dmg*-1);

            if (lives < 1)
            {
                Settings.instance.SetPause(true);
                //TODO: Initialize game end
				QADebugging.staticRef.hasLost = true;
            }
        }
    }

	private static void OnTakeDamage(int dmg)
	{
	}
	private static void OnHeal(int heal_amount)
	{
	}

    public static void Reset()
    {
        lives = 3;
        score = 0;
    }
}
