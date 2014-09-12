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
                //TODO: Initialize game end
				QADebugging.staticRef.hasLost = true;
            }
        }
    }
}
