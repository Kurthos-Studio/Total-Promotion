using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private AimCursor aimCursor;
    private CharacterMovement characterMovement;
    private WeaponManager weaponManager;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        aimCursor = transform.Find("Cursor").GetComponent<AimCursor>();
        characterMovement = GetComponent<CharacterMovement>();
        weaponManager = GetComponent<WeaponManager>();
    }

    public void OnFire(InputValue value)
    {
        weaponManager.Fire(value.Get<float>());
    }

    public void OnMove(InputValue value)
    {
        characterMovement.Accelerate(value.Get<Vector2>());
    }

    public void OnMouseMove(InputValue value)
    {
        aimCursor.Accelerate(value.Get<Vector2>());
    }

    public void OnReload(InputValue value)
    {
        weaponManager.Reload();
    }
}