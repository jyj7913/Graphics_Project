using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{

    bool isPickUp;
    private Behaviour halo;
    private Inventory inven;
    // Start is called before the first frame update
    void Start()
    {
        halo = (Behaviour)GetComponent("Halo");
        halo.enabled = false;
        inven = FindObjectOfType<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPickUp && Input.GetKeyDown(KeyCode.E)) PickUp();
        if(gameObject.name == "key_1")
        {
            Debug.Log(gameObject.transform.position);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
            halo.enabled = true;
            isPickUp = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag.Equals("Player"))
        {
            halo.enabled = false;
            isPickUp = false;
        }
    }


    void PickUp()
    {
        Destroy(gameObject);

        inven.ItemGet();
    }

    public void setActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
