using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    CheckPointManager owner;
    void Start()
    {
        owner = GetComponentInParent<CheckPointManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        owner.SetLastCheckpoint(this);
    }

}
