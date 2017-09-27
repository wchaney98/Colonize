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
        GameManager.SelectedNode.Level = 2;
        GameManager.SelectedNode.Life -= Constants.LEVEL_UP_1_COST;
        GameManager.SelectedNode.VirusResistance = 1;
        GameManager.SelectedNode.MoveSpeed *= (float)1.5;
    }

    public void LevelUp2()
    {
        GameManager.SelectedNode.Level = 3;
        GameManager.SelectedNode.Life -= Constants.LEVEL_UP_2_COST;
        GameManager.SelectedNode.VirusResistance = 2;
        GameManager.SelectedNode.MoveSpeed *= (float)1.5;
    }

    public void LevelUp3()
    {
        GameManager.SelectedNode.Level = 4;
        GameManager.SelectedNode.Life -= Constants.LEVEL_UP_3_COST;
        GameManager.SelectedNode.VirusResistance = 3;
        GameManager.SelectedNode.MoveSpeed *= (float)1.5;
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
        INode node = GameManager.SelectedNode;
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
    }
}