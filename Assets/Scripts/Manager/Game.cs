using System.Linq;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 게임 내에서 사용되는 변수들을 관리하는 정적, 전역 클래스
/// </summary>
public static class Game
{
    [InitializeOnEnterPlayMode]
    public static void OnEnterPlayMode()
    {
        // 모든 static 변수를 초기화한다.
        // 로딩 시간을 절약하기 위해 PlayMode 진입 시 도메인을 다시 로드하지 않도록 설정했는데,
        // 그러면 static 변수가 자동으로 초기화되지 않을 수 있기 때문이다. 
        mainCamera = null;
        playerObject = null;
        playerInfo = null;
    }


    private static Camera mainCamera = null;
    public static Camera MainCamera
    {
        get
        {
            if (mainCamera == null)
            {
                mainCamera = GameObject.FindWithTag("MainCamera")?.GetComponent<Camera>();
            }

            return mainCamera;
        }
    }

    private static GameObject playerObject = null;
    public static GameObject PlayerObject
    {
        get
        {
            if (playerObject == null)
            {
                playerObject = GameObject.FindGameObjectsWithTag("Player")
                    .FirstOrDefault(x => x.name == "Player");
            }

            return playerObject;
        }
    }

    private static PlayerInfo playerInfo = null;
    public static PlayerInfo PlayerInfo
    {
        get
        {
            if (playerInfo == null)
            {
                playerInfo = PlayerObject?.GetComponent<PlayerInfo>();
            }

            return playerInfo;
        }
    }
}
