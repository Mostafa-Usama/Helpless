using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class allItemsClass{
    public string key;
    public int value;
}

public class PlayerReach : MonoBehaviour
{
    public Text InteractText, dialouge;
    public FlashlightManager FlashlightScrpit;
    public Inventory_Manager inventory_Manager;
    public Texture2D cursorHandIcon;
    public allItemsClass [] allItems;
    public Animator dooranim; 
     Dictionary<string, int> itemsDictianory = new Dictionary<string, int>();
    // Start is called before the first frame update
    void Start()
    {
        InteractText.enabled =false;
        dialouge.enabled = false;
        foreach (var item in allItems)
        {
           itemsDictianory.Add(item.key, item.value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerStay(Collider col){
        if (col.CompareTag("Batteries")){
            InteractText.text = "Pick Up Batteries\n [E]";
            InteractText.enabled = true;
            col.GetComponent<Outline>().enabled = true;
           // Cursor.SetCursor(cursorHandIcon,Vector2.zero, CursorMode.Auto);
            //Cursor.visible = true;
            if (Input.GetKeyDown(KeyCode.E)){
                InteractText.enabled = false;
              //  Cursor.visible = false;
                //FlashlightScrpit.BatteryLife += 50;
                dialouge.text = "You picked up a battery";
                StartCoroutine("ShowDialouge");
                
                inventory_Manager.addItem(itemsDictianory[col.tag]);
                Destroy(col.gameObject);
            }
            
                
            }
        if (col.CompareTag("door"))
        {
            Debug.Log("aaaaaaaaaaaaaaaaaaa");
            bool isopen = col.GetComponent<doorscript>().open;
            bool locked = col.GetComponent<doorscript>().locked;
            dooranim = col.GetComponent<Animator>();

            if (Input.GetKeyDown(KeyCode.E) && !locked)
            {
                if (!isopen)
                {
                    dooranim.SetTrigger("open");
                    dooranim.ResetTrigger("close");

                    col.GetComponent<doorscript>().open = true;
                }
                else
                {
                    dooranim.SetTrigger("close");
                    dooranim.ResetTrigger("open");
                    col.GetComponent<doorscript>().open = false;

                }

            }
            else
            {
                dialouge.text = "Door is Locked";
                StartCoroutine("ShowDialouge");
            }
        }
        
    }
    void OnTriggerExit(Collider col){
       // Cursor.visible = false;
        InteractText.enabled = false;
        if ( col.GetComponent<Outline>()!= null){
             col.GetComponent<Outline>().enabled = false;
        }
    }
    IEnumerator ShowDialouge(){
        dialouge.enabled = true;
        yield return new WaitForSeconds(5f);
        dialouge.enabled = false;
    }
}
