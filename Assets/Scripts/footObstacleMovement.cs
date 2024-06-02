using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footObstacleMovement : MonoBehaviour
{
    public float topY = 5.0f; // Engel nesnesinin ulaşacağı en yüksek nokta
    public float speed = 2.0f; // Engel nesnesinin yukarı hareket hızı
    private bool movingUp = true; // Engel nesnesinin hareket yönünü kontrol eder

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (movingUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);

            if (transform.position.y >= topY)
            {
                movingUp = false;
                rb.gravityScale = 1; // Yer çekimini etkinleştir
            }
        }
        else
        {
            if (rb.velocity.y == 0) // Engel nesnesi yere düştüğünde
            {
                movingUp = true;
                rb.gravityScale = 0; // Yer çekimini devre dışı bırak
                rb.velocity = new Vector2(rb.velocity.x, speed);
            }
        }
    }
}