using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public float followDistance = 5f; // Takip mesafesi

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private float initialY; // Düşmanın başlangıçtaki y pozisyonu

    private bool isPlayerInRange = false;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        initialY = transform.position.y; // Düşmanın başlangıçtaki y pozisyonunu kaydet
    }

    private void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        // Hedefin pozisyonunu al
        Vector2 targetPosition = new Vector2(target.position.x, initialY);

        // Düşman ile oyuncu arasındaki mesafeyi hesapla
        float distanceToTarget = Vector2.Distance(rb.position, new Vector2(target.position.x, rb.position.y));

        // Eğer oyuncu takip mesafesi içinde ise
        if (distanceToTarget <= followDistance && !isPlayerInRange)
        {
            // Hedefe doğru hareket et
            Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.deltaTime);
            rb.MovePosition(newPosition);

            // Düşmanın yüzünün yönünü değiştir
            if (target.position.x < transform.position.x)
            {
                // Düşman sola bakmalı
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                // Düşman sağa bakmalı
                transform.localScale = new Vector3(1, 1, 1);
            }

            // isRunning animasyonunu tetikle
            anim.SetBool("isRunning", true);
        }
        else
        {
            // Düşman duruyor, isRunning animasyonunu durdur
            anim.SetBool("isRunning", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // isAttacking animasyonunu tetikle
            anim.SetBool("isAttacking", true);
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // isAttacking animasyonunu durdur
            anim.SetBool("isAttacking", false);
            isPlayerInRange = false;
        }
    }
}
