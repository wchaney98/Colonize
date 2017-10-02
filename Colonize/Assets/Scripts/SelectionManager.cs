using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class SelectionManager : MonoBehaviour
{
    public GameObject GameManagerObject;
    public Texture lineTexture;

    private GameManager gameManager;
    private Vector2 originalPos;
    private List<Vector2> points;
    private VectorLine selectionRect;
    
    void Start()
    {
        gameManager = GameManagerObject.GetComponent<GameManager>();
        points = new List<Vector2>(5);
        selectionRect = new VectorLine("Selection", points, 2f, LineType.Continuous)
        {
            textureScale = 4.0f
        };
    }

    void Update()
    {
        if (gameManager.PlayerState == PlayerState.FREE)
        {
            if (Input.GetMouseButtonDown(0))
            {
                originalPos = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                selectionRect.active = true;
                selectionRect.MakeRect(originalPos, Input.mousePosition);
                selectionRect.Draw();
            }
            if (Input.GetMouseButtonUp(0))
            {
                selectionRect.active = false;
                Rect bounds = new Rect(originalPos.x, originalPos.y, Input.mousePosition.x - originalPos.x, Input.mousePosition.y - originalPos.y);
                if (Mathf.Abs(bounds.size.x * bounds.size.y) > Constants.SELECTION_MIN_SIZE)
                {
                    gameManager.ProcessSelection(originalPos, Input.mousePosition);
                }
            }
        }
    }
}
