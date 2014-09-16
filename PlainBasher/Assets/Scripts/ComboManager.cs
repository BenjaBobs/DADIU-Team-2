using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComboManager : MonoBehaviour
{

    #region Static stuff
    static ComboManager staticRef;
    void Awake()
    {
        staticRef = this;
    }
    #endregion

    static int scoreMultiplier = 1;
    static List<PointText> comboObjects = new List<PointText>();
    static float comboTime = 0.2f;

    void Update()
    {

    }


    public static void StartCombo(PointText starter)
    {
        comboObjects.Add(starter);
    }

    public static void AddChain(PointText chainer)
    {
        scoreMultiplier++;
        comboObjects.Add(chainer);

        foreach (PointText pointText in comboObjects)
        {
            pointText.scoreMultiplier = scoreMultiplier;
        }
    }

    //Jellies don't increase the multiplier
    public static void AddCasualty(PointText casualty)
    {
        comboObjects.Add(casualty);
    }
    
    static void EndCombo()
    {
        int totalScore = 0;

        foreach (PointText pointText in comboObjects)
        {
            totalScore += pointText.scoreValue;
        }

        totalScore *= scoreMultiplier;

        Player.Score += totalScore;

        scoreMultiplier = 1;
    }

}
