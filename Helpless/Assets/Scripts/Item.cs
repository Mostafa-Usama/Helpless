using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Image icon;
    public string itemType; // For categorizing items (e.g., Consumable, Tool)
    
    [TextArea]
    public string description;
}
