using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDrop : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Zehir damlasını yok et
        Destroy(gameObject);
    }
}
