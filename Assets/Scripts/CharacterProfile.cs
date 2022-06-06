using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterProfile : MonoBehaviour
{
    public bool IsPlayer;

    private int healthpoints;
    [SerializeField]
    private int rankNumber;
    private TMPro.TMP_Text tagText;
    private WeaponManager weaponManager;

    private void Awake()
    {
        tagText = transform.Find("Canvas/Rank").GetComponent<TMPro.TMP_Text>();
        weaponManager = GetComponent<WeaponManager>();
    }

    private void Start()
    {
        ChangeRank(rankNumber);
    }

    private void OnEnable()
    {
        EventManager.AddTypedListener("BulletHitCharacter", OnBulletHitCharacter);
    }

    private void OnDisable()
    {
        EventManager.RemoveTypedListener("BulletHitCharacter", OnBulletHitCharacter);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            EventManager.TriggerTypedEvent("BulletHitCharacter", new CustomEventData(new HitData(
                rankNumber, collision.GetComponent<Bullet>().Owner
            )));
            if (tag == "NPC")
            {
                Destroy(gameObject);
            }
            if (tag == "Player")
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    private void OnBulletHitCharacter(CustomEventData data)
    {
        if (data.hit.Offender == gameObject && data.hit.VictimRank > rankNumber)
        {
            Promote();
        }
    }

    public void ChangeRank(int number)
    {
        rankNumber = number;
        var rank = Globals.Ranks[number];
        healthpoints = rank.Healthpoints;
        weaponManager.EquipWeapon(rank.Weapon, rank.WeaponClipSize, rank.WeaponCooldown);
        tagText.text = rank.GetTag();
        tagText.color = rank.Color;

        if (IsPlayer)
        {
            EventManager.TriggerTypedEvent("UpdatePlayerRank", new CustomEventData(rank.Label));
            EventManager.TriggerTypedEvent("UpdatePlayerHealth", new CustomEventData(healthpoints));
        } 
    }

    public void Promote()
    {
        if (rankNumber < Globals.Ranks.Length - 1)
        {
            ChangeRank(rankNumber + 1);
        } else
        {
            Debug.Log("Already the highest rank.");
        }
    }
}
