using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public bool IsOnPlayer;
    public int CurrentAmmo;

    private Animator animator;
    private float bulletCooldown;
    private float bulletTime;
    private int clipSize;
    private Transform bulletSpawn;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        bulletSpawn = transform.Find("SpriteWrapper/BulletSpawn");
    }

    public void EquipWeapon(string weapon, int size, float cooldown)
    {
        clipSize = size;
        CurrentAmmo = size;
        this.bulletCooldown = cooldown;
        if (IsOnPlayer)
        {
            EventManager.TriggerTypedEvent("UpdatePlayerClipSize", new CustomEventData(clipSize));
            EventManager.TriggerTypedEvent("UpdatePlayerCurrentAmmo", new CustomEventData(CurrentAmmo));
            EventManager.TriggerTypedEvent("UpdatePlayerWeapon", new CustomEventData(weapon));
        }
    }

    public bool Fire(float inputValue)
    {
        if (inputValue > 0 && CurrentAmmo > 0 && bulletTime + bulletCooldown < Time.realtimeSinceStartup)
        {
            bulletTime = Time.realtimeSinceStartup;
            animator.SetTrigger("Fire");
            var bullet = Instantiate(Resources.Load("Bullet"), bulletSpawn.position, bulletSpawn.rotation) as GameObject;
            bullet.GetComponent<Bullet>().Owner = gameObject;
            CurrentAmmo--;
            if (IsOnPlayer)
            {
                EventManager.TriggerTypedEvent("UpdatePlayerCurrentAmmo", new CustomEventData(CurrentAmmo));
            }
            return true;
        }
        return false;
    }

    public void Reload()
    {
        if (CurrentAmmo < clipSize)
        {
            animator.SetTrigger("Reload");
            CurrentAmmo = clipSize;
            if (IsOnPlayer)
            {
                EventManager.TriggerTypedEvent("UpdatePlayerCurrentAmmo", new CustomEventData(CurrentAmmo));
            }
        }
    }
}