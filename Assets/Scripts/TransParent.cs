using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransParent : MonoBehaviour
{
    private BoxCollider collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTrue()
    {
        collider.enabled = true;
    }

    public void SetFalse()
    {
        collider.enabled = false;
    }
}
