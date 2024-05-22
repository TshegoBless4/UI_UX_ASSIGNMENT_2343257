using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public RectTransform canvasRectTransform;
    public GameObject startPositionObject;
    public GameObject endPositionObject;
    public float moveSpeed = 5f;

    private Vector2 startPosition;
    private Vector2 endPosition;
    private bool moveToEnd = true;

    private void Start()
    {
        canvasRectTransform = GetComponent<RectTransform>();

        startPosition = startPositionObject.transform.position;
        endPosition= endPositionObject.transform.position;
    }

    private void Update()
    {
        Vector2 targetPosition = moveToEnd ? endPosition : startPosition;
        canvasRectTransform.anchoredPosition = Vector2.Lerp(canvasRectTransform.anchoredPosition,targetPosition,moveSpeed * Time.deltaTime);
    }

    public void ToggleCanvasMovement()
    {
        moveToEnd = !moveToEnd;
    }
}
