using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class GameHud : MonoBehaviour
{
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI spText;

    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (Game.PlayerInfo == null)
        {
            canvasGroup.alpha = 0;
            return;
        }

        canvasGroup.alpha = 1;
        hpText.text = $"{Game.PlayerInfo.currentHP}";
        spText.text = $"{Game.PlayerInfo.currentSP:0.0}";
    }
}
