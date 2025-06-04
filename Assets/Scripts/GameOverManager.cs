using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // GameOverPanel 연결
    public GameObject firstSelectedButton; // GameOverPanel에서 처음 선택될 버튼 연결
    public PlayerInput playerInput; // 인스펙터에서 PlayerInput 연결

    private bool isGameOver = false;

    public void TriggerGameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        gameOverPanel.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);

        Time.timeScale = 0f; // 게임 일시정지
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Stage1_new"); // Stage1_new 씬 로드
    }

    public void ExitToTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title"); // Title 씬 로드
    }
}