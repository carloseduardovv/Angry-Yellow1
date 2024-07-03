using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float maxDistance;
    [SerializeField] private AudioClip dragSound; // Sonido para cuando se jala el pájaro
    [SerializeField] private AudioClip shootSound; // Sonido para cuando el pájaro es lanzado

    private Camera mainCamera;
    private Rigidbody2D rb;
    private Vector2 startPosition, clampedPosition;
    private Vector3 offset;
    private AudioSource audioSource;

    private void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        startPosition = transform.position;

        audioSource = GetComponent<AudioSource>(); // Inicializa el AudioSource
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Si no hay AudioSource, lo agrega
        }
    }

    private void OnMouseDown()
    {
        offset = transform.position - (Vector3)mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Reproduce el sonido del audio cuando se jala el pájaro
        if (dragSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(dragSound);
        }
    }

    private void OnMouseDrag()
    {
        Vector2 dragPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Clamp drag position to maxDistance from startPosition
        float dragDistance = Vector2.Distance(startPosition, dragPosition);
        if (dragDistance > maxDistance)
        {
            dragPosition = startPosition + (dragPosition - startPosition).normalized * maxDistance;
        }

        transform.position = new Vector3(dragPosition.x, dragPosition.y, transform.position.z);
        clampedPosition = dragPosition;
    }

    private void OnMouseUp()
    {
        rb.isKinematic = false;
        Vector2 throwVector = startPosition - (Vector2)transform.position;
        rb.AddForce(throwVector * force, ForceMode2D.Impulse);

        // Reproduce el sonido del audio cuando el pájaro es lanzado
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
}
