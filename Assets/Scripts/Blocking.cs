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

    public void LongCollider()                                                                  // If 2D, player's body can step on or touch the objects
    {
        originalLength = collider.size.x;
        collider.size = new Vector3(collider.size.x * 30, collider.size.y, collider.size.z);
    }

    public void ShortCollider()                                                                 // If 3D, objects exist as they are looked.
    {

        collider.size = new Vector3(originalLength, collider.size.y, collider.size.z);
    }
}
