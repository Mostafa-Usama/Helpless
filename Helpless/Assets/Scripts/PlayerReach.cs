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
     bool inReach = false;
     GameObject colGame = null;
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
         if (Input.GetButtonDown("interact") && inReach){
            Interact(colGame);
         }
    }


    void Interact(GameObject gameObj){
        if (gameObj.CompareTag("Batteries")){
            InteractText.enabled = false;
              //  Cursor.visible = false;
                //FlashlightScrpit.BatteryLife += 50;
                StopCoroutine("ShowDialouge");
                dialouge.text = "You picked up a battery";
                StartCoroutine("ShowDialouge");
                
                inventory_Manager.addItem(itemsDictianory[gameObj.tag]);
                Destroy(gameObj);
        }
       if (gameObj.CompareTag("door")){
            bool isopen = gameObj.GetComponent<doorscript>().isOpen;
            bool locked = gameObj.GetComponent<doorscript>().isLocked;
            dooranim = gameObj.GetComponent<Animator>();
            // InteractText.enabled = true;
            //InteractText.text = isopen? "Close Door\n [E]" : "Open Door\n [E]" ;
            gameObj.GetComponent<doorscript>().playsound();
            if (!locked){

                if (!isopen)
                {
                    dooranim.SetBool("isOpened", true);
                    dooranim.SetBool("isClosed", false);

                }
                else
                {
                    dooranim.SetBool("isOpened", false);
                    dooranim.SetBool("isClosed", true);
                }
                gameObj.GetComponent<doorscript>().isOpen = !gameObj.GetComponent<doorscript>().isOpen;
                Debug.Log(gameObj.GetComponent<doorscript>().isOpen.ToString());
            }
            else
            {
                StopCoroutine("ShowDialouge");
                dialouge.text = "Door is Locked";
                StartCoroutine("ShowDialouge");
            }
        }
    }
   
    

    void OnTriggerStay(Collider col){
        if (col.CompareTag("Batteries")){
            InteractText.text = "Pick Up Batteries\n [E]";
            InteractText.enabled = true;
            col.GetComponent<Outline>().enabled = true;
            inReach = true;
            colGame = col.gameObject;
                
            }
        if (col.CompareTag("door"))
        {
            inReach = true;
            colGame = col.gameObject;
            bool isopen = col.GetComponent<doorscript>().isOpen;
            InteractText.enabled = true;
            InteractText.text = isopen? "Close Door\n [E]" : "Open Door\n [E]" ;
        }
        
    }
    void OnTriggerExit(Collider col){
       // Cursor.visible = false;
        InteractText.enabled = false;
        if ( col.GetComponent<Outline>()!= null){
             col.GetComponent<Outline>().enabled = false;
        }
        inReach = false;
    }
    IEnumerator ShowDialouge(){
        dialouge.enabled = true;
        yield return new WaitForSeconds(5f);
        dialouge.enabled = false;
    }
}
