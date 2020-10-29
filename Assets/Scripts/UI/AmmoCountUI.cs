using UnityEngine;
using UnityEngine.UI;

public class AmmoCountUI : MonoBehaviour
{
    public PoolManager pm;
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
        pm.OnCountUpdate.AddListener(UpdateCountUI);
        text.text = pm.currentCount + "/" + pm.maxCount;
    }

    void UpdateCountUI()
    {
        text.text = pm.currentCount + "/" + pm.maxCount;
    }
}
