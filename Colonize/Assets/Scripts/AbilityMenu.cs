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
}
