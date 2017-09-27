﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMenu : MonoBehaviour
{

    public GameObject Camera;
    public GameManager GameManager;

    public GameObject SlowdownButton;
    public GameObject SurplusButton;
    public GameObject DestroyVirusesButton;
    public GameObject TenTimesButton;

    private float SlowdownCooldownRemaining = 0f;
    private float SurplusCooldownRemaining = 0f;
    private float DestroyVirusesCooldownRemaining = 0f;
    private float TenTimesCooldownRemaining = 0f;

    public void SlowdownAbility()
    {
        if (SlowdownCooldownRemaining <= 0f)
        {
            GameManager.SlowdownTime();
            SlowdownCooldownRemaining = Constants.ABILITY_SLOWDOWN_COOLDOWN;
        }
    }

    public void SurplusAbility()
    {
        if (SurplusCooldownRemaining <= 0f)
        {
            GameManager.GiveLifeToAll(Constants.ABILITY_SURPLUS_AMOUNT);
            SurplusCooldownRemaining = Constants.ABILITY_SURPLUS_COOLDOWN;
        }
    }

    public void DestroyVirusesAbility()
    {
        if (DestroyVirusesCooldownRemaining <= 0f)
        {
            GameManager.ResetViruses();
            DestroyVirusesCooldownRemaining = Constants.ABILITY_DESTROY_VIRUSES_COOLDOWN;
        }
    }

    public void TenTimesAbility()
    {
        if (TenTimesCooldownRemaining <= 0f)
        {
            Persistence.existing.TenTimesAbilityActive = true;
            TenTimesCooldownRemaining = Constants.ABILITY_TEN_TIMES_COOLDOWN;
        }
    }

    void Start()
    {
        GameManager = Camera.GetComponent<GameManager>();
    }

    private void Update()
    {
        // Check if timers etc allow buttons to be active... use radial fills for cooldown
        if (SlowdownCooldownRemaining >= 0f)
        {
            SlowdownCooldownRemaining -= Time.deltaTime;
            SlowdownButton.GetComponent<Image>().fillAmount = (Constants.ABILITY_SLOWDOWN_COOLDOWN - SlowdownCooldownRemaining) / Constants.ABILITY_SLOWDOWN_COOLDOWN;
        }

        if (SurplusCooldownRemaining >= 0f)
        {
            SurplusCooldownRemaining -= Time.deltaTime;
            SurplusButton.GetComponent<Image>().fillAmount = (Constants.ABILITY_SURPLUS_COOLDOWN - SurplusCooldownRemaining) / Constants.ABILITY_SURPLUS_COOLDOWN;
        }

        if (DestroyVirusesCooldownRemaining >= 0f)
        {
            DestroyVirusesCooldownRemaining -= Time.deltaTime;
            DestroyVirusesButton.GetComponent<Image>().fillAmount = (Constants.ABILITY_DESTROY_VIRUSES_COOLDOWN - DestroyVirusesCooldownRemaining) / Constants.ABILITY_DESTROY_VIRUSES_COOLDOWN;
        }

        if (TenTimesCooldownRemaining >= 0f)
        {
            TenTimesCooldownRemaining -= Time.deltaTime;
            TenTimesButton.GetComponent<Image>().fillAmount = (Constants.ABILITY_TEN_TIMES_COOLDOWN - TenTimesCooldownRemaining) / Constants.ABILITY_TEN_TIMES_COOLDOWN;
        }

        DestroyVirusesButton.GetComponent<Button>().interactable = Persistence.existing.Time >= 60f;
        
    }
}