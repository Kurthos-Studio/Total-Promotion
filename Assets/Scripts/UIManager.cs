using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Transform PlayerPanel;
    public Transform WeaponPanel;

    private TMPro.TMP_Text playerClipSizeText;
    private TMPro.TMP_Text playerCurrentAmmoText;
    private TMPro.TMP_Text playerHealthText;
    private TMPro.TMP_Text playerRankText;
    private TMPro.TMP_Text playerWeaponText;

    private void Awake()
    {
        playerClipSizeText = WeaponPanel.Find("Ammo/ClipSize").GetComponent<TMPro.TMP_Text>();
        playerCurrentAmmoText = WeaponPanel.Find("Ammo/CurrentAmmo").GetComponent<TMPro.TMP_Text>();
        playerHealthText = PlayerPanel.Find("Health/Healthpoints").GetComponent<TMPro.TMP_Text>();
        playerRankText = PlayerPanel.Find("Rank").GetComponent<TMPro.TMP_Text>();
        playerWeaponText = WeaponPanel.Find("Weapon").GetComponent<TMPro.TMP_Text>();
    }

    private void OnEnable()
    {
        EventManager.AddTypedListener("UpdatePlayerClipSize", OnUpdatePlayerClipSize);
        EventManager.AddTypedListener("UpdatePlayerCurrentAmmo", OnUpdatePlayerCurrentAmmo);
        EventManager.AddTypedListener("UpdatePlayerHealth", OnUpdatePlayerHealth);
        EventManager.AddTypedListener("UpdatePlayerRank", OnUpdatePlayerRank);
        EventManager.AddTypedListener("UpdatePlayerWeapon", OnUpdatePlayerWeapon);
    }

    private void OnDisable()
    {
        EventManager.RemoveTypedListener("UpdatePlayerClipSize", OnUpdatePlayerClipSize);
        EventManager.RemoveTypedListener("UpdatePlayerCurrentAmmo", OnUpdatePlayerCurrentAmmo);
        EventManager.RemoveTypedListener("UpdatePlayerHealth", OnUpdatePlayerHealth);
        EventManager.RemoveTypedListener("UpdatePlayerRank", OnUpdatePlayerRank);
        EventManager.RemoveTypedListener("UpdatePlayerWeapon", OnUpdatePlayerWeapon);
    }

    private void OnUpdatePlayerClipSize(CustomEventData data)
    {
        playerClipSizeText.text = data.intContent.ToString();
    }

    private void OnUpdatePlayerCurrentAmmo(CustomEventData data)
    {
        playerCurrentAmmoText.text = data.intContent.ToString();
    }

    private void OnUpdatePlayerHealth(CustomEventData data)
    {
        playerHealthText.text = data.intContent.ToString();
    }

    private void OnUpdatePlayerRank(CustomEventData data)
    {
        playerRankText.text = data.stringContent;
    }

    private void OnUpdatePlayerWeapon(CustomEventData data)
    {
        playerWeaponText.text = data.stringContent;
    }

}