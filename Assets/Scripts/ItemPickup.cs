using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemDataScriptable assignedItem;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PickUp();
        }
    }

    public void PickUp()
    {
        InventoryUI.assignedInventory.OnItemPickup(assignedItem);
        gameObject.SetActive(false); // Ukryj obiekt po podniesieniu
    }
}
