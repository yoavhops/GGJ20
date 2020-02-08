
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

    public float effectOnMaxHot;
    public float effectOnMaxSalt;
    public float effectOnMaxHydration;
    public float effectOnMaxHeight;

    public float effectOnMinHot;
    public float effectOnMinHydration;
    public float effectOnMinHeight;

    public float effectOnNutrients;
    public float effetOnDamp;

    public float effectOnGrowthRate;
    public float SlowHumans = 0;


    private Tree _treeDiffuse;

    public int Price;

    private Image skillImage; 

    private void CheckStateStatus()
    {
        if(skillState == SkillState.Learned)
        {
            return;
        }

        if (DoesUserRequiredSkills())
        {
            OnAvailable();
            return;
        }

        skillState = SkillState.Locked;
        SyncColor();
    }

    private void Awake()
    {
        skillImage = GetComponent<Image>();

    }

    private void Start()
    {
        CheckStateStatus();
        SyncColor();
        _treeDiffuse = (Tree)GridManager.Singleton.TypeToDiffuse[typeof(Tree)];
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
        if (skillState != SkillState.Learned)
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

        PointManager.singleton.addPoints(-Price);
        Debug.Log("Learned SKILL!");

        SyncColor();

        FireSkillEffects();

    }

    private void FireSkillEffects()
    {
        //TODO:: add maxtree value that will affect TODO in Tree
        _treeDiffuse.TreeMaxHeight += effectOnMaxHeight;
        _treeDiffuse.TreeMaxHydration += effectOnMaxHydration;
        _treeDiffuse.TreeMaxSalt += effectOnMaxSalt;
        _treeDiffuse.TreeMaxTemp += effectOnMaxHot;

        _treeDiffuse.TreeMinHeight -= effectOnMinHeight;
        _treeDiffuse.TreeMinHydration -= effectOnMinHydration;
        _treeDiffuse.TreeMinTemp -= effectOnMinHot;
    }

    void OnAvailable()
    {
        skillState = SkillState.Available;
        Debug.Log("Available SKILL!");
        SyncColor();
    }

    private void SyncColor()
    {
        switch (skillState)
        {
            case SkillState.Available:
                skillImage.color = ColorSkillSetting.singleton.colorAvailable;
                break;
            case SkillState.Learned:
                skillImage.color = ColorSkillSetting.singleton.colorLearned;
                break;
            case SkillState.Locked:
                skillImage.color = ColorSkillSetting.singleton.colorLocked;
                break;
        }
    }

    private bool DoesUserRequiredSkills()
    {

        for (int i = 0; i < requiredSkills.Count; i++)
        {
            if (requiredSkills[i].skillState != SkillState.Learned)
            {
                return false;
            }
        }

        return true;
    }

    private bool DoesUserHaveEnoughPoints()
    {
        return Price <= PointManager.singleton.getPoints();
    }



    public enum SkillState
    {
        Available,
        Locked,
        Learned

    }
}