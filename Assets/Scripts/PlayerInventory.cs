using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static Action<ItemDataScriptable> itemPickup;

    // Lista przedmiotów
    public List<Item> items;
    public int maxItems = 9;

    private void Awake()
    {
        items = new List<Item>(maxItems);
        for (int i = 0; i < maxItems; i++)
        {
            items.Add(null); // Wype³niamy pustymi slotami
        }

        itemPickup += OnItemPickup;
        InventoryUI.assignedInventory = this;
    }

    private void OnDestroy()
    {
        itemPickup -= OnItemPickup;
    }

    private void Update()
    {
        for (int i = 0; i < maxItems; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                if (i < items.Count && items[i] != null)
                {
                    items[i].Use();
                }
            }
        }
    }

    public void OnItemPickup(ItemDataScriptable itemData)
    {
        for (int i = 0; i < items.Count; i++)
        {
            // ZnajdŸ pierwszy pusty slot i przypisz przedmiot
            if (items[i] == null)
            {
                Item item = new Item
                {
                    assignedItem = itemData,
                    quantity = 1
                };

                items[i] = item;
                InventoryUI.refreshInventory?.Invoke();
                Debug.Log("Picked up item: " + item.assignedItem.name);
                return;
            }
        }

        Debug.Log("Inventory full, cannot pick up item.");
    }
}

public class Item
{
    // Przypisany scriptable z danymi przedmiotu
    public ItemDataScriptable assignedItem;

    // Iloœæ przedmiotów
    public int quantity;

    public void Use()
    {
        if (assignedItem == null) return;

        if (assignedItem is Item_ConsumableSO consumable)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                if (player.TryGetComponent<Health>(out var healthComponent) && consumable.health > 0)
                {
                    healthComponent.Heal(consumable.health);
                }

                if (player.TryGetComponent<MyCharacterController>(out var controller) && consumable.jumpHeight > 0)
                {
                    controller.SetTemporaryJumpBoost(consumable.jumpHeight, 999999f);
                }
            }

            quantity--;
            if (quantity <= 0)
            {
                // ZnajdŸ i wyczyœæ slot
                for (int i = 0; i < InventoryUI.assignedInventory.items.Count; i++)
                {
                    if (InventoryUI.assignedInventory.items[i] == this)
                    {
                        InventoryUI.assignedInventory.items[i] = null;
                        break;
                    }
                }
            }

            InventoryUI.refreshInventory?.Invoke();
        }
    }
}
