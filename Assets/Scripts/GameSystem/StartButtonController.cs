using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonController : MonoBehaviour
{
    private bool isButtonClicked = false;

    void Start()
    {
        // 버튼 컴포넌트 가져와서 클릭 이벤트 등록
        Button startButton = GetComponent<Button>();
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }
    }

    void Update()
    {
        // 버튼이 클릭된 후 Z 키 입력 시 Stage1_new 씬으로 이동
        if (isButtonClicked && Input.GetKeyDown(KeyCode.Z))
        {
            SceneManager.LoadScene("Stage1_new");
        }
    }

    void OnStartButtonClicked()
    {
        isButtonClicked = true; // 버튼 클릭 감지
    }
}
