using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHighlighter : MonoBehaviour
{
    public Button[] buttons;
    public Color highlightColor = Color.white;
    private Color defaultColor;

    void Start()
    {
        defaultColor = buttons[0].GetComponent<Image>().color;
        
        // 버튼 선택 이벤트 등록
        foreach (Button btn in buttons)
        {
            btn.interactable = true;
            EventTrigger trigger = btn.gameObject.AddComponent<EventTrigger>();
            
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener((data) => OnButtonSelected(btn));
            
            trigger.triggers.Add(entry);
        }
    }

    void OnButtonSelected(Button selectedButton)
    {
        foreach (Button btn in buttons)
        {
            btn.GetComponent<Image>().color = (btn == selectedButton) ? highlightColor : defaultColor;
        }
    }
}
