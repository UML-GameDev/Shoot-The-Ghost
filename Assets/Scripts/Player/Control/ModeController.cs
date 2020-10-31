using UnityEngine;

public class ModeController : MonoBehaviour
{
    public InputManager im;
    public GameObject[] modes;
    int index = 0;

    //TODO- Change Sprite at Diffent Place
    SpriteRenderer playerSprite;
    public Sprite[] sprites;

    void Awake()
    {
        playerSprite = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        im.switchModeEvent += Switch;
        for (int i = 0; i < modes.Length; ++i)
        {
            modes[index].SetActive(true);
        }
        playerSprite.sprite = sprites[index];
    }
    void OnDisable()
    {
        im.switchModeEvent -= Switch;
        foreach (GameObject mode in modes) mode.SetActive(false);
    }

    void Switch()
    {
        //switch the mode 
        modes[index].SetActive(false);
        if (++index >= modes.Length) index = 0; 
        modes[index].SetActive(true);
        playerSprite.sprite = sprites[index];
    }
}
