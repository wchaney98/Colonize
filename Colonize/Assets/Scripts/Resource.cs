using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour 
{
    public Sprite[] AnimSprites;
    public int AnimStopIndex;
    public float AnimFrameSpeed;
    public INode Gatherer { get; set; }

    private SpriteRenderer spriteRenderer;
    private int currSpriteId = 0;

    private float RelocateTimer = 0f;
    private float RelocateTime = 8f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Gatherer = null;
    }

    void Start () 
	{
        StartCoroutine(Animate(AnimSprites, AnimStopIndex, AnimFrameSpeed));
	}

    private void Update()
    {
        RelocateTimer += Time.deltaTime;
        if (RelocateTimer >= RelocateTime)
        {
            transform.position = new Vector3(Random.Range(-8f, 4f), Random.Range(-4f, 4f), transform.position.z);
            RelocateTimer = 0f;
        }
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
}
