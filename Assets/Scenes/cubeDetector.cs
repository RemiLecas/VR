using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("onCollision()");
        if (collision.gameObject.name == "Key")
        {
            Debug.Log("Cube bleu sur Cube rouge !");
        }
    }
}
