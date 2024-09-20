using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerReach : MonoBehaviour
{
    public Text InteractText, dialouge;
    public FlashlightManager FlashlightScrpit;
    // Start is called before the first frame update
    void Start()
    {
        InteractText.enabled =false;
        dialouge.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerStay(Collider col){
        if (col.CompareTag("Batteries")){
            InteractText.text = "Pick Up Batteries\n [E]";
            InteractText.enabled = true;
            if (Input.GetKeyDown(KeyCode.E)){
            InteractText.enabled = false;
                FlashlightScrpit.BatteryLife += 50;
                dialouge.text = "You picked up a battery";
                StartCoroutine("ShowDialouge");
                Destroy(col.gameObject);
            }
        }
    }
    void OnTriggerExit(Collider col){
        InteractText.enabled = false;
    }
    IEnumerator ShowDialouge(){
        dialouge.enabled = true;
        yield return new WaitForSeconds(5f);
        dialouge.enabled = false;
    }
}
