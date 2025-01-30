using UnityEngine;

public class Road : MonoBehaviour
{
    GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if((Player.transform.position.z - this.transform.position.z) > 25)
        {
            Destroy(this.gameObject);
        }
    }
}
