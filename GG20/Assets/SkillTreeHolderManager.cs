using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeHolderManager : MonoBehaviour
{
    public void SkillButtonClicked()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
}
