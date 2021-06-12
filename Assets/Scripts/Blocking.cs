using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : MonoBehaviour
{
    private BoxCollider collider;
    private float originalLength;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        originalLength = collider.size.x;
        collider.size = new Vector3(collider.size.x * 30, collider.size.y, collider.size.z);
    }

    public void LongCollider()
    {
        originalLength = collider.size.x;
        collider.size = new Vector3(collider.size.x * 30, collider.size.y, collider.size.z);
    }

    public void ShortCollider()
    {

        collider.size = new Vector3(originalLength, collider.size.y, collider.size.z);
    }
}
