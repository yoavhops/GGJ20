
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class Skill : MonoBehaviour
{

    public SkillState skillState = SkillState.Locked;

    public List<Skill> requiredSkills;
    public bool isLocked = true;


    private int points = 10; //TODO::get actual points

    private bool isLearned = false;
    public int Price;

    public bool IsLearned { get { return isLearned; } }

    private Image skillImage; 

    private void handleStatus()
    {

    }

    private void Start()
    {
        skillState = SkillState.Locked;
        skillImage = GetComponent<Image>();

        skillImage.color = ColorSkillSetting.singleton.colorLocked;
    }

    private void Update()
    {

        if(skillState == SkillState.Locked)
        {
           if( DoesUserRequiredSkills())
            {
                OnAvailable();
            }
        }

    }

    public void OnClick()
    {
        if (!isLearned)
        {

            if (skillState == SkillState.Available)
            {
                if (DoesUserHaveEnoughPoints())
                {
                    OnLearned();
                }
                else
                {
                    Debug.Log("Not Enough Money");
                }
            }
        }
    }



    void OnLearned()
    {
        skillState = SkillState.Learned;

        isLearned = true;
        points -= Price; //TODO::
        Debug.Log("Learned SKILL!");

        skillImage.color = ColorSkillSetting.singleton.colorLearned;

    }

    void OnAvailable()
    {
        skillState = SkillState.Available;
        Debug.Log("Available SKILL!");
        skillImage.color = ColorSkillSetting.singleton.colorAvailable;

    }

    private bool DoesUserRequiredSkills()
    {

        for (int i = 0; i < requiredSkills.Count; i++)
        {
            if (!requiredSkills[i].IsLearned)
            {
                return false;
            }
        }

        return true;
    }

    private bool DoesUserHaveEnoughPoints()
    {
        return Price <= points;
    }



    public enum SkillState
    {
        Available,
        Locked,
        Learned

    }
}