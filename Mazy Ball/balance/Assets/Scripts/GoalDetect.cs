using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GoalDetect : MonoBehaviour {
    
    bool firstCollision = true;
    public static Texture2D ssTexture;
    void OnCollisionEnter(Collision collision)
    {
        if (firstCollision == true)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = new Color(56, 238, 244);
            Handheld.Vibrate();
            Util.win = true;
            Util.endingTime = Time.time;
            ssTexture = ScreenCapture.CaptureScreenshotAsTexture();
            firstCollision = false;
            StartCoroutine(wait(0.3f));
        }
    }
    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(Util.gameOverScene);
    }
}
