using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiController : MonoBehaviour
{
    private bool followPlayer;
    private float fireCooldown;
    private float fireTime;
    private int fireBurstAmount;

    private AimCursor aimCursor;
    private CharacterMovement characterMovement;
    private NavMeshAgent agent;
    private Transform target;
    private WeaponManager weaponManager;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        aimCursor = transform.Find("Cursor").GetComponent<AimCursor>();
        characterMovement = GetComponent<CharacterMovement>();
        weaponManager = GetComponent<WeaponManager>();
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        fireBurstAmount = Random.Range(3, 6);
        fireCooldown = Random.Range(0.5f, 2f);
    }

    private void Update()
    {
        var aimDirection = target.position - transform.position;
        

        if (followPlayer)
        {
            aimCursor.Accelerate(aimDirection);
            agent.SetDestination(target.position - aimDirection.normalized * Random.Range(5, 10));
        }

        if (aimDirection.magnitude < 12)
        {
            followPlayer = true;
        } else
        {
            followPlayer = false;
        }

        if (aimDirection.magnitude < 10)
        {
            if (fireBurstAmount > 0)
            {
                if (weaponManager.Fire(1))
                {
                    fireBurstAmount--;
                    fireTime = Time.realtimeSinceStartup;
                }
            } else
            {
                if (fireTime + fireCooldown < Time.realtimeSinceStartup)
                {
                    fireBurstAmount = Random.Range(3, 6);
                    fireCooldown = Random.Range(0.5f, 2f);
                }
            }
        }

        if (weaponManager.CurrentAmmo < 1)
        {
            weaponManager.Reload();
        }
    }

}
