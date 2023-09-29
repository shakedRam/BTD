using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool isDragging = false;
    private string grassTag = "Grass";
    [SerializeField] Transform spawnPos;

    private void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            transform.position = mousePosition;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().size, 0);
        
        bool droppedInGrass = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(grassTag))
            {
                droppedInGrass = true;
                break;
            }
        }

        if (!droppedInGrass)
        {
            transform.position = spawnPos.transform.position; // Reset the cannon's position
        }
    }
}