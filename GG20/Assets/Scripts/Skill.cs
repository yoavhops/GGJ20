
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class Skill : MonoBehaviour
{
    public List<Skill> requiredSkills;
    public bool isLocked = true;

    public float effectOnHot;
    public float effectOnCold;
    public float effectOnSalt;
    public float effectOnHydration;
    public float effectOnHeight;
    public float effectOnNutrients;

    private int points = 10; //TODO::get actual points

    private bool isLearned = false;
    public int Price;

    public bool IsLearned { get { return isLearned; } }

    private void handleStatus()
    {
        
    }

    public void OnClick()
    {
        if (!isLearned)
        {

            bool isAvailable = true;

            for (int i = 0; i < requiredSkills.Count; i++)
            {
                if (!requiredSkills[i].IsLearned)
                {
                    isAvailable = false;
                }
            }


            if (isAvailable)
            {
                if (DoesUserHaveEnoughPoints())
                {
                    isLearned = true;
                    points -= Price; //TODO::
                    Debug.Log("Learned SKILL!");
                }
                else
                {
                    Debug.Log("Not Enough Money");
                }
            }
        }
    }

    private bool DoesUserHaveEnoughPoints()
    {
        return Price <= points;
    }
}