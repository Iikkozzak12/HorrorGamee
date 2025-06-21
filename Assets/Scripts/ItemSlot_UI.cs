using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot_UI : MonoBehaviour
{
    public TextMeshProUGUI quantity;
    public Image itemIcon;

    public void SetSlot(ItemDataScriptable assignedItem)
    {
        if (assignedItem == null)
        {
            quantity.text = "";
            itemIcon.sprite = null; // Nie ustawiamy ikony, bo przedmiot jest pusty
            itemIcon.color = new Color(1, 1, 1, 0.3f); // Ustawiamy przezroczysty kolor na ikonie, je�li slot jest pusty
            itemIcon.enabled = false;
        }
        else
        {
            quantity.text = "1"; // Zak�adamy na razie 1 dla ka�dego przedmiotu
            itemIcon.sprite = assignedItem.icon;
            itemIcon.color = Color.white; // Ustawiamy pe�n� widoczno�� ikony
            itemIcon.enabled = true;
        }
    }
}
