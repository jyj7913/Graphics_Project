using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransParent : MonoBehaviour
{
    private BoxCollider[] collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponents<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTrue()
    {
        for (int i = 0; i < collider.Length; i++)
        {
            collider[i].enabled = true;
        }
    }

    public void SetFalse()
    {
        for (int i = collider.Length - 1; i > -1; i--)
        {
            collider[i].enabled = false;
        }
    }
}
