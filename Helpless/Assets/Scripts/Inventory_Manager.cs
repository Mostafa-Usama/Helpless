using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_Manager : MonoBehaviour
{
    public List<Item> allItems;
    public Image toolSlot;
    public Image [] itemSlots;
    public Dictionary<int, int> itemAmount = new Dictionary<int, int>(); // key = index of item, value = amount of this item
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addItem(int index){
        foreach(Image slot in itemSlots){
            if (slot.gameObject.transform.childCount != 0){
                //Instantiate(itemIcons[index], slot.gameObject.transform);
                Transform child = slot.gameObject.transform.GetChild(0);
                if (child.CompareTag(allItems[index].itemName)){
                    itemAmount[index]++;
                    GameObject number = child.GetChild(0).gameObject;
                    number.SetActive(true);
                    number.GetComponent<Text>().text = itemAmount[index].ToString();
                    Debug.Log( itemAmount[index]);
                    return;
                }
               
            }
        }
        foreach(Image slot in itemSlots){
            if (slot.gameObject.transform.childCount == 0){
                    Instantiate(allItems[index].icon, slot.gameObject.transform);
                    itemAmount[index] = 1;
                    Debug.Log( itemAmount[index]);
                    return;
                }
        }
    }
}
