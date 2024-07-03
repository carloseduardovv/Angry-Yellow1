using UnityEngine;

public class Monster1 : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el objeto con el que colisionamos es un Bird
        Bird bird = collision.gameObject.GetComponent<Bird>();

        // Si bird no es nulo, significa que hemos colisionado con un Bird
        if (bird != null)
        {
            Destroy(gameObject); // Destruir este objeto Monster1
        }
        float crushThreshold = -0.5f;
        bool isCrushed = collision.contacts[0].normal.y < crushThreshold;

        if (isCrushed)
        {
            Destroy(gameObject);
        }

    }
}
