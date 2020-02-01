using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSkillSetting : MonoBehaviour
{

    public Color colorAvailable;
    public Color colorLocked;
    public Color colorLearned;


    public static ColorSkillSetting singleton;

    private void Awake()
    {
        singleton = this;
    }


}
