using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComboManager : MonoBehaviour
{

    #region Static stuff
    static ComboManager staticRef;
    void Awake()
    {
        if (!staticRef)
            staticRef = this;
    }
    #endregion

    static List<PointText> comboObjects = new List<PointText>();
    static float comboTime = 0.1f;
    static bool isComboing = false;

    void Update()
    {
        if (isComboing)
        {
            comboTime -= Settings.instance.GetDeltaTime();
            if (comboTime < 0)
                EndCombo();
        }
    }

    public static void AddChain(PointText chainer)
    {
        comboObjects.Add(chainer);

        foreach (PointText pointText in comboObjects)
        {
            pointText.scoreMultiplier = comboObjects.Count;
        }
    }

    public static void StartChain()
    {
        isComboing = true;
    }
    
    static void EndCombo()
    {
        isComboing = false;
        comboTime = 0.1f;

        int totalScore = 0;

        foreach (PointText pointText in comboObjects)
        {
            totalScore += pointText.scoreValue;
        }

        totalScore *= comboObjects.Count;
        comboObjects.Clear();

        Player.Score += totalScore;

    }

}
