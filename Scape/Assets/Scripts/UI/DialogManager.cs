using TMPro;
using UnityEngine;

public class DialogManager : Singleton<DialogManager>
{

    [SerializeField] private TextMeshProUGUI tip;

    protected override void Awake()
    {
        base.Awake();
        HideTip();
    }

    public static void ShowTip(string tipText)
    {
        instance.tip.text = tipText;
        instance.tip.gameObject.SetActive(true);
    }

    public static void HideTip()
    {
        instance.tip.text = "";
        instance.tip.gameObject.SetActive(false);
    }

}
