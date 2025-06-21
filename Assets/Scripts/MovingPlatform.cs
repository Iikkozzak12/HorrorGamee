using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Points (Z-axis only)")]
    public List<float> zPositions = new List<float> { 0f, 5f, 10f };

    [Header("Settings")]
    public float speed = 2f;
    public float stopTime = 1f;

    private int currentTargetIndex = 0;
    private bool isWaiting = false;

    void Update()
    {
        if (zPositions.Count == 0 || isWaiting) return;

        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, zPositions[currentTargetIndex]);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            StartCoroutine(WaitBeforeNextPoint());
        }
    }

    System.Collections.IEnumerator WaitBeforeNextPoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(stopTime);

        currentTargetIndex = (currentTargetIndex + 1) % zPositions.Count;
        isWaiting = false;
    }
}
