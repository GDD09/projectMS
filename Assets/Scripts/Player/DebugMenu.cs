using UnityEngine;

public class DebugMenu : MonoBehaviour
{
    void OnGUI()
    {
        if (!Debug.isDebugBuild)
        {
            return;
        }

        GUI.Label(new Rect(20, 20, 200, 20), "Debug Menu");

        // Game Timescale
        var timescale = GUI.HorizontalSlider(new Rect(20, 40, 200, 20), Time.timeScale, 0.0f, 2.0f);
        GUI.Label(new Rect(20, 50, 200, 20), $"Speed: x{timescale:0.00}");
        Time.timeScale = timescale;

        // Clear all bullets
        if (GUI.Button(new Rect(20, 70, 200, 20), "Clear All Bullets"))
        {
            var bullets = GameObject.FindGameObjectsWithTag("Bullet");
            foreach (var bullet in bullets)
            {
                Destroy(bullet);
            }
        }
    }
}
