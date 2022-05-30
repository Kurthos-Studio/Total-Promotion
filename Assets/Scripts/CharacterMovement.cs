using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

#pragma warning disable CS0108 // Member hides inherited member; missing new keyword

public class CharacterMovement : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private float force;
    private Rigidbody2D rigidbody;
    private Transform aimCursor;
    private Transform spriteTransform;
    private Vector2 acceleration;

    private void Awake()
    {
        aimCursor = transform.Find("Cursor");
        animator = transform.Find("SpriteWrapper/Sprite").GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteTransform = transform.Find("SpriteWrapper");
    }

    private void FixedUpdate()
    {
        Rotation();
        Movement();
    }

    private void Movement()
    {
        rigidbody.AddRelativeForce(spriteTransform.rotation * acceleration);
        animator.SetFloat("Acceleration", acceleration.magnitude);
    }

    private void Rotation()
    {
        var angle = Vector2.SignedAngle(Vector2.up, aimCursor.localPosition);
        spriteTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Accelerate(Vector2 direction)
    {
        acceleration = force *  direction;
    }
}