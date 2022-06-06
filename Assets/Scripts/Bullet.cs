using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword

public class Bullet : MonoBehaviour
{
    public float Lifetime = 0.5f;
    public float Speed = 1.0f;
    public GameObject Owner;

    private void Start()
    {
        StartCoroutine(DieAfterTime());
    }

    private void FixedUpdate()
    {
        transform.position += transform.up.normalized * Speed;
    }

    private IEnumerator DieAfterTime()
    {
        yield return new WaitForSeconds(Lifetime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
