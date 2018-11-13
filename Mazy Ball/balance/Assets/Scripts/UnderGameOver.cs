using UnityEngine;
using UnityEngine.SceneManagement;
public class UnderGameOver : MonoBehaviour {
    bool firstCollision = true;
    public static Texture2D ssTexture;
    void OnCollisionEnter(Collision collision)
    {
        if (firstCollision == true)
        {
            Util.win = false;
            Util.endingTime = Time.time;
            ssTexture = ScreenCapture.CaptureScreenshotAsTexture();
            firstCollision = false;
            SceneManager.LoadScene(Util.gameOverScene);
        }
    }
}
