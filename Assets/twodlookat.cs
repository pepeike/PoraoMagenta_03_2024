using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twodlookat : MonoBehaviour
{

    public Camera maincam;
    void Update()
    {
        transform.LookAt(maincam.transform);
    }
}
