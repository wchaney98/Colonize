using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpMenu : MonoBehaviour
{
    public GameObject Camera;
    public GameManager GameManager;

    public GameObject LevelOneObject;
    public GameObject LevelTwoObject;
    public GameObject LevelThreeObject;

    private Button[] levelUpButtons = new Button[3];
    private CanvasGroup canvasGroup;

    public void LevelUp1()
    {
        foreach(INode node in GameManager.SelectedNodes)
        {
            node.Level = 2;
            node.Life -= Constants.LEVEL_UP_1_COST;
            node.VirusResistance = 1;
            node.MoveSpeed *= (float)1.5;
        }
    }

    public void LevelUp2()
    {
        foreach (INode node in GameManager.SelectedNodes)
        {
            node.Level = 3;
            node.Life -= Constants.LEVEL_UP_2_COST;
            node.VirusResistance = 2;
            node.MoveSpeed *= (float)1.5;
        }
    }

    public void LevelUp3()
    {
        foreach (INode node in GameManager.SelectedNodes)
        {
            node.Level = 4;
            node.Life -= Constants.LEVEL_UP_3_COST;
            node.VirusResistance = 3;
            node.MoveSpeed *= (float)1.5;
        }
    }

    void Start()
    {
        GameManager = Camera.GetComponent<GameManager>();
        levelUpButtons[0] = LevelOneObject.GetComponent<Button>();
        levelUpButtons[1] = LevelTwoObject.GetComponent<Button>();
        levelUpButtons[2] = LevelThreeObject.GetComponent<Button>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        foreach (INode node in GameManager.SelectedNodes)
        {
            if (node != null && node.Level <= 3)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;
                foreach (Button b in levelUpButtons)
                {
                    if (b != levelUpButtons[node.Level - 1])
                    {
                        b.interactable = false;
                    }
                }
                levelUpButtons[node.Level - 1].interactable = true;
            }
            else
            {
                foreach (Button b in levelUpButtons)
                {
                    b.interactable = false;
                }
                canvasGroup.alpha = 0f;
                canvasGroup.interactable = false;
            }
            if (node != null && Input.GetKeyDown(Persistence.existing.Controls["LevelUpKey"]))
            {
                switch (node.Level)
                {
                    case 1:
                        node.Level = 2;
                        node.Life -= Constants.LEVEL_UP_1_COST;
                        node.VirusResistance = 1;
                        node.MoveSpeed *= (float)1.5;
                        break;
                    case 2:
                        node.Level = 3;
                        node.Life -= Constants.LEVEL_UP_2_COST;
                        node.VirusResistance = 2;
                        node.MoveSpeed *= (float)1.5;
                        break;
                    case 3:
                        node.Level = 4;
                        node.Life -= Constants.LEVEL_UP_3_COST;
                        node.VirusResistance = 3;
                        node.MoveSpeed *= (float)1.5;
                        break;
                }
            }
        }
    }
}