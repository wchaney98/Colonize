using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VirusState
{
    IDLE,
    CHOOSING,
    CHARGE,
    CHARGING
}

public class Virus : MonoBehaviour 
{
    private GameObject Cam;

    private List<INode> nodes;
    private VirusState state = VirusState.IDLE;

    private float choosingTimer = 0;
    private float choosingTime = 2f;

    private float chargingSpeed = 3f;
    private int chargeTarget = 0;
    private Vector2 chargeTargetPos;

    void Start () 
	{
        Cam = GameObject.Find("Main Camera");
        nodes = Cam.GetComponent<GameManager>().NodeManager.Nodes;
        Cam.GetComponent<GameManager>().Viruses.Add(this);
    }
	
	void Update () 
	{
        if (state == VirusState.IDLE)
        {
            if (nodes.Count != 0)
            {
                if (choosingTimer < choosingTime)
                {
                    choosingTimer += Time.deltaTime;
                }
                else
                {
                    state = VirusState.CHOOSING;
                    choosingTimer = 0;
                }
            }
        }

        if (state == VirusState.CHOOSING)
        {
            chargeTarget = Random.Range(0, nodes.Count);
            chargeTargetPos = nodes[chargeTarget].Position;
            state = VirusState.CHARGE;
        }

        if (state == VirusState.CHARGE)
        {
            Vector2 dir = chargeTargetPos - (Vector2)transform.position;
            dir.Normalize();
            StartCoroutine(Charge(chargeTargetPos, dir));
            state = VirusState.CHARGING;
        }

        if (state == VirusState.CHARGING)
        {
            if (Vector2.Distance(transform.position, chargeTargetPos) < 0.1f)
            {
                StopAllCoroutines();
                state = VirusState.IDLE;
            }
        }
	}

    IEnumerator Charge(Vector2 newPos, Vector2 dir)
    {
        while (Vector2.Distance(transform.position, newPos) > 0.1f)
        {
            Vector2 temp = transform.position;
            temp += dir * chargingSpeed * Time.deltaTime;
            transform.position = temp;
            yield return new WaitForEndOfFrame();
        }
    }
}
