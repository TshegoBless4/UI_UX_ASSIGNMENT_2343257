using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    public float buyingPrice = 10f; // Price when buying from shop
    public float sellingPrice = 5f; // Price when selling to shop

    private bool isDragging = false;
    private bool hasBeenBought = false; // Flag to track if the object has been bought
    private Vector2 offset;

    private void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        SnapToNearestPosition();
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = GetMouseWorldPosition();
            transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, transform.position.z);
        }
    }

    private void SnapToNearestPosition()
    {
        Collider2D[] snapColliders = FindObjectsOfType<Collider2D>();

        Collider2D closestCollider = null;
        float closestDistance = Mathf.Infinity;
        Vector2 snapPosition = Vector2.zero;

        foreach (Collider2D collider in snapColliders)
        {
            if (collider.gameObject != gameObject) // Skip self
            {
                Vector2 center = collider.bounds.center;
                float distance = Vector2.Distance(transform.position, center);
                if (distance < closestDistance)
                {
                    closestCollider = collider;
                    closestDistance = distance;
                    snapPosition = center;
                }
            }
        }

        if (closestCollider != null && closestDistance <= 0.5f) // Use a fixed threshold for snapping
        {
            switch (closestCollider.tag)
            {
                case "SellSnap":
                    SellItem();
                    break;
                case "ShopSnap":
                    BuyItem();
                    break;
                    // Add other cases for different snap point tags if needed
            }

            transform.position = snapPosition;
        }
    }

    private void BuyItem()
    {
        if (!hasBeenBought) // Check if the object hasn't been bought yet
        {
            DragAndDrop dragAndDropScript = FindObjectOfType<DragAndDrop>();
            if (dragAndDropScript != null)
            {
                dragAndDropScript.BuyItem(buyingPrice);
                hasBeenBought = true; // Set the flag to indicate the object has been bought
            }
        }
        else
        {
            Debug.Log("This item has already been bought!");
        }
    }

    private void SellItem()
    {
        if (hasBeenBought) // Check if the object has been bought
        {
            DragAndDrop dragAndDropScript = FindObjectOfType<DragAndDrop>();
            if (dragAndDropScript != null)
            {
                dragAndDropScript.RefundItem(sellingPrice); // Refund the money based on selling price
                hasBeenBought = false; // Mark the item as not bought

                // Detach the object from the snap point
                transform.position += new Vector3(0.1f, 0.1f, 0); // Example adjustment to move the object slightly
            }
        }
        else
        {
            Debug.Log("This item hasn't been bought yet, so it cannot be sold.");
        }
    }



    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
