using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class cubeDetector : MonoBehaviour
{
    [SerializeField] ParticleSystem ExplosionEffect;
    // Start is called before the first frame update
    void Start()
    {
        XRSettings.enabled = true;
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
            TriggerExplosion(collision.contacts[0].point);
        }
    }

    void TriggerExplosion(Vector3 position)
    {
        Debug.Log("TriggerExplosion()");
        ExplosionEffect.Play();
    }
}
