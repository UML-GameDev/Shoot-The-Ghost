using UnityEngine;

/*
 * Weapon Switcher
 * 
 */
public class WeaponSwitcher : MonoBehaviour
{
    public UIManager uim;
    public GameObject[] weapons;
    private int previousIndex = 0;
    public GameObject ui;

    void OnEnable()
    {
        previousIndex = uim.currentIndex;
        uim.switchPlayerWeaponEvent += Switch;
        for (int i = 0; i < weapons.Length; ++i)
        {
            if (i == previousIndex) weapons[i].SetActive(true);
            else weapons[i].SetActive(false);
        }
        ui.SetActive(true);
    }
    void OnDisable()
    {
        uim.switchPlayerWeaponEvent -= Switch;
        foreach (GameObject weapon in weapons) { weapon.SetActive(false); }
        ui.SetActive(false);
    }

    void Switch(int index)
    {
        weapons[previousIndex].SetActive(false);
        weapons[index].SetActive(true);

        previousIndex = index;
    }
}
