using UnityEngine;

public class RoadDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Road"))
        {
            Destroy(other.gameObject);
        }
    }
}
