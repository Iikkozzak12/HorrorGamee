using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager instance;
    public CheckPoint lastCheckpoint;
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    public void SetLastCheckpoint(CheckPoint checkPoint)
    {
        lastCheckpoint = checkPoint;
    }
}
