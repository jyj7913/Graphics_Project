using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

    public List<SlotData> slots = new List<SlotData>();
    public Text keyNum;
    public int n;

    private void Start()
    {
        SlotData slot = new SlotData();
        slot.isEmpty = true;


        keyNum = GetComponent<Text>();
        keyNum.text = "Key: " + n + "/5";
    }
}