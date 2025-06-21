using UnityEngine;

public class Lava : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        DealDamage();
    }

    public void DealDamage()
    {

        player.gameObject.GetComponent<Health>().TakeDamage(200);

    }
}
