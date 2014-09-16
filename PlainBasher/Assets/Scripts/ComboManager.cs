using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ComboManager {

    public static int scoreMultiplier = 1;
    static List<GameObject> comboObjects = new List<GameObject>();


    public void StartCombo(GameObject starter)
    {
        comboObjects.Add(starter);
    }

    public void AddChain(GameObject chainer)
    {
        scoreMultiplier++;
        comboObjects.Add(chainer);

        foreach (GameObject pointText in comboObjects)
        {
            pointText.GetComponent<PointText>().scoreMultiplier = scoreMultiplier;
        }
    }

    //Jellies don't increase the multiplier
    public void AddCasualty(GameObject casualty)
    {
        comboObjects.Add(casualty);
    }
    
    void EndCombo()
    {
        int totalScore = 0;

        foreach (GameObject pointText in comboObjects)
        {
            totalScore += pointText.GetComponent<PointText>().scoreValue;
        }

        totalScore *= scoreMultiplier;

        Player.Score += totalScore;

        scoreMultiplier = 1;
    }

}
