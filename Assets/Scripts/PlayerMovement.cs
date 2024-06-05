using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator anim;
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float climbSpeed = 5f; // Duvar tırmanma hızı
    [SerializeField] float reloadTime = 1f;
    private bool isClimbing = false;
    private bool isDead = false;

    bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (canMove == true)
        {
        Run();
        UpdateAnimator();
        FlipSprite();
       
        
        }
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

void OnCollisionEnter2D(Collision2D collision)
{
    // Check if the character collides with an obstacle
    if (collision.collider.CompareTag("Obstacle"))
    {
        isDead = true;
        Die();
        anim.SetBool("isDead", isDead);
        DisableControls();
        Invoke("ReloadScene", reloadTime);
    }
    // Check if the character collides with poison
    if (collision.collider.CompareTag("poison")) 
    {
        isDead = true;
        Die();
        anim.SetBool("isDead", isDead);
        DisableControls();
       Invoke("ReloadScenee", reloadTime);
    }
     if (collision.collider.CompareTag("foot")) 
    {
        isDead = true;
        Die();
        anim.SetBool("isDead", isDead);
        DisableControls();
       Invoke("ReloadScenee", reloadTime);
    }
     if (collision.collider.CompareTag("venom")) 
    {
        isDead = true;
        Die();
        anim.SetBool("isDead", isDead);
        DisableControls();
       Invoke("ReloadSceneee", reloadTime);
    }
      if (collision.collider.CompareTag("water")) 
    {
        isDead = true;
        Die();
        anim.SetBool("isDead", isDead);
        DisableControls();
       Invoke("ReloadSceneeee", reloadTime);
    }
}

void ReloadSceneeee()
{
    // Assuming "MainScene" is the name of the scene you want to load
    SceneManager.LoadScene(3);
    
}
void ReloadSceneee()
{
    // Assuming "MainScene" is the name of the scene you want to load
    SceneManager.LoadScene(2);
    
}
void ReloadScenee()
{
    // Assuming "MainScene" is the name of the scene you want to load
    SceneManager.LoadScene(3);
    
}


    public void DisableControls()
    {
        canMove = false;
    }


    void Die()
    {
        
         isDead = true;
        Debug.Log("die");
    }



    void ReloadScene()
    {
            SceneManager.LoadScene(4);

    }


}