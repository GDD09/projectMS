using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    public Button startButton; // 첫 버튼 연결

    public GameObject controlPanel; // Control 안내문구 패널
    public GameObject optionPanel;  // Option 안내문구 패널

    private bool isControlVisible = false;
    private bool isOptionVisible = false;

    void Start()
    {
        GameObject startObj = GameObject.Find("StartButton");
        if (startObj != null)
        {
            EventSystem.current.SetSelectedGameObject(startObj);
        }
        else
        {
            Debug.LogWarning("StartButton 오브젝트를 찾을 수 없습니다.");
        }

        // 패널들 처음엔 숨기기
        if (controlPanel != null)
            controlPanel.SetActive(false);
        if (optionPanel != null)
            optionPanel.SetActive(false);

    }
    void Update()
    {
        // Z 키
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // 우선 ControlPanel 표시 중이면 닫기
            if (isControlVisible)
            {
                controlPanel.SetActive(false);
                isControlVisible = false;
            }
            // OptionPanel 표시 중이면 닫기
            else if (isOptionVisible)
            {
                optionPanel.SetActive(false);
                isOptionVisible = false;
            }
            // 아무 패널도 없으면 → 현재 선택된 버튼 기능 실행
            else
            {
                if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject != null)
                {
                    GameObject selected = EventSystem.current.currentSelectedGameObject;
                    Button btn = selected.GetComponent<Button>();
                    if (btn != null)
                    {
                        btn.onClick.Invoke();
                    }
                }
            }
        }
    }

    public void OnStartButtonClicked()
    {
        Debug.Log("Start Stage1_new");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Stage1_new");
    }

    public void OnControlButtonClicked()
    {
        Debug.Log("Control 설명 화면 표시");
        if (controlPanel != null)
        {
            controlPanel.SetActive(true);
            isControlVisible = true;
        }
    }

    public void OnOptionButtonClicked()
    {
        Debug.Log("Option 기능 미구현 표시");
        if (optionPanel != null)
        {
            optionPanel.SetActive(true);
            isOptionVisible = true;
        }
    }

    public void OnExitButtonClicked()
    {
        Application.Quit();
        Debug.Log("게임 종료");
    }
}
