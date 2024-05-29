using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator anim;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float climbSpeed = 5f; // Duvar tırmanma hızı
    private bool isClimbing = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        UpdateAnimator();
         FlipSprite();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Run()
    {
        Vector2 playerVelocity;

        if (isClimbing)
        {
            // Tırmanma durumundayken hem dikey hem yatay hareketi ayarla
            playerVelocity = new Vector2(moveInput.x * runSpeed, moveInput.y * climbSpeed);
        }
        else
        {
            // Yürüme durumundayken sadece yatay hareketi ayarla
            playerVelocity = new Vector2(moveInput.x * runSpeed, rb.velocity.y);
        }

        rb.velocity = playerVelocity;

        // Karakterin yüzünün doğru yöne bakmasını sağla
        if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void UpdateAnimator()
    {
        // Yürüme hareketi kontrolü
        bool isWalking = !isClimbing && Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        anim.SetBool("isRunning", isWalking);

        // Tırmanma hareketi kontrolü
        anim.SetBool("isClimbing", isClimbing);
    }

   void FlipSprite()
    {
        if (!isClimbing) // Tırmanırken sprite'ı çevirmeyelim
        {
            bool playerHasHorizontalSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
            if (playerHasHorizontalSpeed)
            {
                transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            // Engelle temas edildiğinde tırmanma durumunu aktif et
            isClimbing = true;
            rb.gravityScale = 0; // Duvarlardayken yer çekimini kapat

            // Tırmanma durumundayken hem dikey hem yatay hızı ayarla
            rb.velocity = new Vector2(moveInput.x * runSpeed, moveInput.y * climbSpeed);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            // Engelden ayrılınca tırmanma durumunu devre dışı bırak
            isClimbing = false;
            rb.gravityScale = 1; // Yere döndüğünde yer çekimini tekrar aç
        }
    }
}
