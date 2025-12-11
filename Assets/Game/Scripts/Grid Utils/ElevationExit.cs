using UnityEngine;

public class ElevationExit : MonoBehaviour
{
   
    public Collider2D[] mountainColliders;
    public Collider2D[] boundaryColliders;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (var mountain in mountainColliders)
            {
                Debug.Log($"Exit => mountain => {mountain.enabled}");
                mountain.enabled = true;
                
            }

            foreach (var boundary in boundaryColliders)
            {
                Debug.Log($"Exit => boundary => {boundary.enabled}");
                boundary.enabled = false;
                
            }

            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }
    }
}
