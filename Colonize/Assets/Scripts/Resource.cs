using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour 
{
    public Sprite[] AnimSprites;
    public int AnimStopIndex;
    public float AnimFrameSpeed;

    private SpriteRenderer spriteRenderer;
    private int currSpriteId = 0;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start () 
	{
        StartCoroutine(Animate(AnimSprites, AnimStopIndex, AnimFrameSpeed));
	}
	
    IEnumerator Animate(Sprite[] sprites, int stopIndex, float speed)
    {
        while (true)
        {
            spriteRenderer.sprite = sprites[currSpriteId++];
            if (currSpriteId > stopIndex)
            {
                currSpriteId = 0;
            }
            yield return new WaitForSeconds(speed);
        }
        
    }

	void Update () 
	{

	}
}
