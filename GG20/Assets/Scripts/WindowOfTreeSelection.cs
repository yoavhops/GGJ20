using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowOfTreeSelection : MonoBehaviour
{
    public GameObject PanelFullScreen;
    public GameObject SkillPanel;
    public GameObject background;
    public GameObject backgroundFrame;
    private bool panelCheck;
    public Button skillTree;
    void Start()
    {
        panelCheck = false;

        Button btn = skillTree.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    private void DisablePanel()
    {
        PanelFullScreen.SetActive(false);
        panelCheck = false;

    }

    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            if (panelCheck == false)
            {

                
                PanelFullScreen.SetActive(true);
                panelCheck = true;
            }
        }

        if (panelCheck == true)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                DisablePanel();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (panelCheck == true)
            {

                //Get the mouse position on the screen and send a raycast into the game world from that position.
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);



                //If something was hit, the RaycastHit2D.collider will not be null.

                if (hit.collider != null)
                {
                    if (hit.collider.name == "PanelFullScreen")
                    {
                        Debug.Log(hit.collider.name);
                        Debug.Log("PanelFullScreen");
                        DisablePanel();
                    }

                    else if (hit.collider.name == "Image")
                    {

                        Debug.Log("Image");
                    }
                     
                }

            }
        }

    }

    void TaskOnClick()
    {
        if (SkillPanel.activeInHierarchy)

            {
            SkillPanel.SetActive(false);
            background.SetActive(false);
            backgroundFrame.SetActive(false);
        }
        else
           {
            SkillPanel.SetActive(true);
            background.SetActive(true);
            backgroundFrame.SetActive(true);
        }
    }

}
