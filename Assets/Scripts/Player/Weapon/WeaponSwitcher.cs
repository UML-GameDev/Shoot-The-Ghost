using UnityEngine;

/*
 * Weapon Switcher
 * 
 */
public class WeaponSwitcher : MonoBehaviour
{
    public UIManager um;
    public GameObject[] weapons;
    private int previousIndex = 0;

    void Awake()
    {
        um.switchPlayerWeaponEvent += Switch;
        previousIndex = um.currentIndex;
    }

    void OnEnable()
    {
        for (int i = 0; i < weapons.Length; ++i)
        {
            if (i == 0) weapons[i].SetActive(true);
            else weapons[i].SetActive(false);
        }
    }
    void OnDisable()
    {
        foreach (GameObject weapon in weapons) { weapon.SetActive(false); }
    }

    void Switch(int index)
    {
        weapons[previousIndex].SetActive(false);
        weapons[index].SetActive(true);

        previousIndex = index;
    }
}
