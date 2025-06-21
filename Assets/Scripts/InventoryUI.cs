using System;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // Event, kt�ry wywo�uje si� przy konieczno�ci od�wie�enia widoku ekwipunku
    public static Action refreshInventory;

    // Przypisane inventory gracza
    public static PlayerInventory assignedInventory;

    // Prefab slotu z itemem
    public GameObject itemSlotPrefab;

    // Transform, pod kt�rym b�d� spawnowane nowe UI sloty
    public Transform itemContainer;

    // Lista przechowuj�ca sloty
    private ItemSlot_UI[] slots;

    private void Awake()
    {
        refreshInventory += Refresh;

        // Tworzymy 9 slot�w na pocz�tku
        slots = new ItemSlot_UI[9];
        for (int i = 0; i < 9; i++)
        {
            GameObject slotObject = Instantiate(itemSlotPrefab);
            ItemSlot_UI slot = slotObject.GetComponent<ItemSlot_UI>();
            slot.transform.SetParent(itemContainer, false);
            slot.gameObject.transform.position = Vector3.zero;
            slots[i] = slot;
        }
    }

    private void OnDestroy()
    {
        refreshInventory -= Refresh;
    }

    void Refresh()
    {
        // Upewniamy si�, �e sloty s� odpowiednio zaktualizowane
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < assignedInventory.items.Count && assignedInventory.items[i] != null)
            {
                // Je�li mamy przedmiot w ekwipunku, aktualizujemy slot
                slots[i].SetSlot(assignedInventory.items[i].assignedItem);
            }
            else
            {
                // Je�li slot jest pusty, ustawiamy go jako pusty
                slots[i].SetSlot(null);
            }
        }
    }
}
