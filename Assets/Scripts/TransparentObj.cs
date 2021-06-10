using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObj : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetTrue()
    {
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void SetFalse()
    {
        transform.localScale = new Vector3(0, 0, 0);
    }
}
