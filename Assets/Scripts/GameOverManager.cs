using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // GameOverPanel 연결 (없으면 자동 Find)
    public GameObject firstSelectedButton; // 처음 선택될 버튼 연결
    public PlayerInput playerInput; // PlayerInput 연결 (없으면 자동 Find)

    private bool isGameOver = false;

    public void TriggerGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        // GameOverPanel 자동 찾기
        if (gameOverPanel == null)
        {
            gameOverPanel = GameObject.Find("GameOverPanel");
            if (gameOverPanel == null)
            {
                Debug.LogWarning("[GameOverManager] GameOverPanel 을 찾지 못했습니다!");
                return;
            }
        }

        gameOverPanel.SetActive(true);

        // 첫 버튼 선택
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);

        // 게임 일시정지
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Stage1_new");
    }

    public void ExitToTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }
}
