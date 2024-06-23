using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    private float length;
    private float startpos;

    private Transform cam;

    public float ParallaxEffect;



    

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float Repos = cam.transform.position.x * (1 - ParallaxEffect);
        float Distance = cam.transform.position.x * ParallaxEffect;

        transform.position = new Vector3(startpos + Distance, transform.position.y, transform.position.z);

        if(Repos > startpos + Length)
        {
            startpos += length;
        }
        else if(Repos < startpos - length)
        {
            startpos -= length;
        }
    }
}

