using UnityEngine;

public class ElevationEntry : MonoBehaviour
{

    public Collider2D[] mountainColliders;
    public Collider2D[] boundaryColliders;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (var mountain in mountainColliders)
            {
                Debug.Log($"Entry => mountain => {mountain.enabled}");
                mountain.enabled = false;

                
            }

            foreach (var boundary in boundaryColliders)
            {
                Debug.Log($"Entry => boundary => {boundary.enabled}");
                boundary.enabled = true;

                
            }

            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }
    }

}
