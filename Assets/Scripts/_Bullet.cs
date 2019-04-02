using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Bullet : MonoBehaviour
{
    public float BulletSpeed;
    private Rigidbody2D rigidbody2D;
    public GameObject BulletEffect;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = new Vector2(BulletSpeed * transform.localScale.x, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(BulletEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
