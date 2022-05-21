using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCursor : MonoBehaviour
{
    [SerializeField]
    private float acceleration = 0.05f;
    [SerializeField]
    private float speed = 5.0f;
    [SerializeField]
    private float maxAnchorOffset = 6.0f;
    [SerializeField]
    private float minAnchorOffset = 3.0f;
    [SerializeField]
    private GameObject playerPawn;

    private Vector3 accelerationDelta;
    private Vector3 pawnOffset;

    private void Start()
    {
        pawnOffset = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, playerPawn.transform.position + pawnOffset, speed * Time.fixedDeltaTime);
    }

    public void Accelerate(Vector2 delta)
    {
        accelerationDelta = new Vector3(delta.x, delta.y, transform.position.z);
        pawnOffset = pawnOffset + (accelerationDelta * acceleration);

        if (pawnOffset.magnitude > maxAnchorOffset)
        {
            pawnOffset = pawnOffset.normalized * maxAnchorOffset;
        }
        else if (pawnOffset.magnitude < minAnchorOffset)
        {
            pawnOffset = pawnOffset.normalized * minAnchorOffset;
        }
    }
}