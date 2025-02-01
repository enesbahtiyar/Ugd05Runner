using UnityEngine;
using DG.Tweening;

public class Collectables : MonoBehaviour
{
    public CollectablesEnum collectablesEnum;
    public int toBeAddedScore;
    public int toBeAddedHealth;
    public int toBeAddedSpeed;
    public GameObject Player;

    private void Start()
    {
        if(collectablesEnum == CollectablesEnum.Coin)
        {
            Player = GameObject.FindFirstObjectByType<PlayerController>().gameObject;
        }
    }

    private void Update()
    {
        if(collectablesEnum == CollectablesEnum.Coin && Player.GetComponent<PlayerController>().isMagnetActive)
        {
            if(Vector3.Distance(Player.transform.position, this.transform.position) < 8)
            {
                transform.DOMove(Player.transform.position + new Vector3(0,1f,0), 0.9f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(other.gameObject);
        }
    }
}
