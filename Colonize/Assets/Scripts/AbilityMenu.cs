using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMenu : MonoBehaviour
{

    public GameObject Camera;
    public GameManager GameManager;

    public void SlowdownAbility()
    {
        
    }

    public void SurplusAbility()
    {

    }

    public void DestroyVirusesAbility()
    {

    }

    public void TenTimesAbility()
    {

    }

    void Start()
    {
        GameManager = Camera.GetComponent<GameManager>();
    }

    private void Update()
    {
        // Check if timers etc allow buttons to be active... use radial fills for cooldown
    }
}
