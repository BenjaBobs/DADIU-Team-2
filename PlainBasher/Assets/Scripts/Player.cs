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
            lives = value;
            if (lives < 1)
            {
                Settings.instance.SetPause(true);
                //TODO: Initialize game end
				QADebugging.staticRef.hasLost = true;
            }
        }
    }

    public static void Reset()
    {
        lives = 3;
        score = 0;
    }
}
