using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] Transform player;

    float yOffset, zOffset;

    private void Start()
    {
        yOffset = transform.position.y - player.position.y;
        zOffset = transform.position.z - player.position.z;
    }
    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.position.z + zOffset);
    }


}
