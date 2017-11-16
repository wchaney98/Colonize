using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpMenu : MonoBehaviour
{
    public GameObject Camera;
    public GameManager GameManager;

    public GameObject[] LevelUpButtonsSpeed = new GameObject[3];
    public GameObject[] LevelUpButtonsDurable = new GameObject[3];

    private Button[] levelUpButtonsSpeed = new Button[3];
    private Button[] levelUpButtonsDurable = new Button[3];

    private CanvasGroup canvasGroup;

    public void LevelUp1Speed()
    {
        foreach (INode node in GameManager.SelectedNodes)
        {
            node.Route = UpgradeRoute.Speed;
            LevelUp(node, Constants.LEVEL_UP_1_COST, node.VirusResistance, node.MoveSpeed * 1.5f);
            node.ShowStar();
        }
    }

    public void LevelUp2Speed()
    {
        foreach (INode node in GameManager.SelectedNodes)
        {
            LevelUp(node, Constants.LEVEL_UP_2_COST, node.VirusResistance, node.MoveSpeed * 1.5f);
            node.ShowStar();
        }
    }

    public void LevelUp3Speed()
    {
        foreach (INode node in GameManager.SelectedNodes)
        {
            LevelUp(node, Constants.LEVEL_UP_3_COST, node.VirusResistance, node.MoveSpeed * 2f);
            node.ShowStar();
        }
    }

    public void LevelUp1Durable()
    {
        foreach (INode node in GameManager.SelectedNodes)
        {
            node.Route = UpgradeRoute.Durable;
            LevelUp(node, Constants.LEVEL_UP_1_COST, node.VirusResistance + 1, node.MoveSpeed);
            node.ShowStar();
        }
    }

    public void LevelUp2Durable()
    {
        foreach (INode node in GameManager.SelectedNodes)
        {
            LevelUp(node, Constants.LEVEL_UP_2_COST, node.VirusResistance + 1, node.MoveSpeed);
            node.ShowStar();
        }
    }

    public void LevelUp3Durable()
    {
        foreach (INode node in GameManager.SelectedNodes)
        {
            LevelUp(node, Constants.LEVEL_UP_3_COST, node.VirusResistance + 2, node.MoveSpeed);
            node.ShowStar();
        }
    }

    private void LevelUp(INode node, int cost, int newResist, float newSpeed)
    {
        node.Level++;
        node.Life -= cost;
        node.VirusResistance = newResist;
        node.MoveSpeed = newSpeed;
        SoundManager.Instance.DoPlayOneShot(SoundFile.LevelUp);
    }

    void Start()
    {
        GameManager = Camera.GetComponent<GameManager>();
        canvasGroup = GetComponent<CanvasGroup>();

        for (int i = 0; i < 3; i++)
        {
            levelUpButtonsSpeed[i] = LevelUpButtonsSpeed[i].GetComponent<Button>();
            levelUpButtonsDurable[i] = LevelUpButtonsDurable[i].GetComponent<Button>();
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
        }
    }

    private void Update()
    {
        if (GameManager.SelectedNodes.Count != 0)
        {
            HashSet<int> noDuplicates = new HashSet<int>
            {
                GameManager.SelectedNodes[0].Level,
                (int)GameManager.SelectedNodes[0].Route
            };
            foreach (INode _node in GameManager.SelectedNodes)
            {
                if (noDuplicates.Add(_node.Level) || noDuplicates.Add((int)_node.Route))
                {
                    // Set all to not interactable
                    canvasGroup.alpha = 0f;
                    canvasGroup.interactable = false;
                    return;
                }
            }

            INode node = GameManager.SelectedNodes[0];

            if (node.Level == 1)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;

                levelUpButtonsSpeed[0].interactable = true;
                levelUpButtonsSpeed[0].GetComponentInChildren<Text>().supportRichText = true;
                levelUpButtonsDurable[0].interactable = true;
                levelUpButtonsDurable[0].GetComponentInChildren<Text>().supportRichText = true;

                levelUpButtonsSpeed[1].interactable = false;
                levelUpButtonsSpeed[1].GetComponentInChildren<Text>().supportRichText = false;
                levelUpButtonsDurable[1].interactable = false;
                levelUpButtonsDurable[1].GetComponentInChildren<Text>().supportRichText = false;

                levelUpButtonsSpeed[2].interactable = false;
                levelUpButtonsSpeed[2].GetComponentInChildren<Text>().supportRichText = false;
                levelUpButtonsDurable[2].interactable = false;
                levelUpButtonsDurable[2].GetComponentInChildren<Text>().supportRichText = false;
            }

            else if (node.Level > 1 && node.Level < 4)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;
                switch (node.Route)
                {
                    case UpgradeRoute.Speed:
                        foreach (Button b in levelUpButtonsSpeed)
                        {
                            if (b != levelUpButtonsSpeed[node.Level - 1])
                            {
                                b.interactable = false;
                                b.GetComponentInChildren<Text>().supportRichText = false;
                            }
                        }
                        levelUpButtonsSpeed[node.Level - 1].interactable = true;
                        levelUpButtonsSpeed[node.Level - 1].GetComponentInChildren<Text>().supportRichText = true;
                        for (int i = 0; i < 3; i++)
                        {
                            levelUpButtonsDurable[i].interactable = false;
                            levelUpButtonsDurable[i].GetComponentInChildren<Text>().supportRichText = false;
                        }
                        break;

                    case UpgradeRoute.Durable:
                        foreach (Button b in levelUpButtonsDurable)
                        {
                            if (b != levelUpButtonsDurable[node.Level - 1])
                            {
                                b.interactable = false;
                                b.GetComponentInChildren<Text>().supportRichText = false;
                            }
                        }
                        levelUpButtonsDurable[node.Level - 1].interactable = true;
                        levelUpButtonsDurable[node.Level - 1].GetComponentInChildren<Text>().supportRichText = true;
                        for (int i = 0; i < 3; i++)
                        {
                            levelUpButtonsSpeed[i].interactable = false;
                            levelUpButtonsSpeed[i].GetComponentInChildren<Text>().supportRichText = false;
                        }
                        break;
                }
            }

            else
            {
                canvasGroup.alpha = 0f;
                canvasGroup.interactable = false;
            }

            if (node != null && InputManager.Instance.LevelUpKey.Check())
            {
                switch (node.Route)
                {
                    case UpgradeRoute.Speed:
                        switch (node.Level)
                        {
                            case 1:
                                LevelUp1Speed();
                                break;
                            case 2:
                                LevelUp2Speed();
                                break;
                            case 3:
                                LevelUp3Speed();
                                break;
                        }
                        break;

                    case UpgradeRoute.Durable:
                        switch (node.Level)
                        {
                            case 1:
                                LevelUp1Durable();
                                break;
                            case 2:
                                LevelUp2Durable();
                                break;
                            case 3:
                                LevelUp3Durable();
                                break;
                        }
                        break;
                }
            }
        }

        //foreach (INode node in GameManager.SelectedNodes)
        //{


        //    if (node != null && node.Level <= 3)
        //    {
        //        canvasGroup.alpha = 1f;
        //        canvasGroup.interactable = true;
        //        foreach (Button b in levelUpButtons)
        //        {
        //            if (b != levelUpButtons[node.Level - 1])
        //            {
        //                b.interactable = false;
        //            }
        //        }
        //        levelUpButtons[node.Level - 1].interactable = true;
        //    }
        //    else
        //    {
        //        foreach (Button b in levelUpButtons)
        //        {
        //            b.interactable = false;
        //        }
        //        canvasGroup.alpha = 0f;
        //        canvasGroup.interactable = false;
        //    }
        //    if (node != null && InputManager.Instance.LevelUpKey.Check())
        //    {
        //        switch (node.Level)
        //        {
        //            case 1:
        //                node.Level = 2;
        //                node.Life -= Constants.LEVEL_UP_1_COST;
        //                node.VirusResistance = 1;
        //                node.MoveSpeed *= (float)1.5;
        //                break;
        //            case 2:
        //                node.Level = 3;
        //                node.Life -= Constants.LEVEL_UP_2_COST;
        //                node.VirusResistance = 2;
        //                node.MoveSpeed *= (float)1.5;
        //                break;
        //            case 3:
        //                node.Level = 4;
        //                node.Life -= Constants.LEVEL_UP_3_COST;
        //                node.VirusResistance = 3;
        //                node.MoveSpeed *= (float)1.5;
        //                break;
        //        }
        //    }
        //}
    }
}