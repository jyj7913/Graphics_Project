using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<SlotData> slots = new List<SlotData>();
    public Text keyNum;
    public int n;

    private void Start()
    {
        SlotData slot = new SlotData();
        slot.isEmpty = true;
        
        
        myText.GetComponent<Text>().text = "Key: " + n + "/5";
    }
}