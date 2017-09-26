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

    private Button[] LevelUpButtons = new Button[3];

    public void LevelUp1()
    {
        GameManager.SelectedNode.Level = 2;
        GameManager.SelectedNode.Life -= 20;
    }

    public void LevelUp2()
    {
        GameManager.SelectedNode.Level = 3;
        GameManager.SelectedNode.Life -= 30;
    }

    public void LevelUp3()
    {
        GameManager.SelectedNode.Level = 4;
        GameManager.SelectedNode.Life -= 40;
    }

    void Start()
    {
        GameManager = Camera.GetComponent<GameManager>();
        LevelUpButtons[0] = LevelOneObject.GetComponent<Button>();
        LevelUpButtons[1] = LevelTwoObject.GetComponent<Button>();
        LevelUpButtons[2] = LevelThreeObject.GetComponent<Button>();
    }

    private void Update()
    {
        INode node = GameManager.SelectedNode;
        if (node != null && node.Level <= 3)
        {
            foreach (Button b in LevelUpButtons)
            {
                b.interactable = false;
            }
            LevelUpButtons[node.Level - 1].interactable = true;
        }
        else
        {
            foreach (Button b in LevelUpButtons)
            {
                b.interactable = false;
            }
        }
    }
}