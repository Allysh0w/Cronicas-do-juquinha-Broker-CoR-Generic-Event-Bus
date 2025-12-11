using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform target;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag =="Player")
        {
            if(StatsManager.Instance.lvl >= 2)
            {
                other.transform.position = target.position;
                other.transform.rotation = target.rotation;
            }
            
        }
    }
}
