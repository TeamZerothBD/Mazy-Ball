using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Util : MonoBehaviour
{
    public static Vector3 lastFloorPosition;
    public static bool helpView = false;
    public static float startingTime;
    public static float endingTime;
    public static int[] scores = { 0, 0, 0, 0 };
    public static bool win;
    public static int type = 1;
    public static Vector3 startingPosition = new Vector3(0,4,0);
    public static int menuScene = 1;
    public static int gameScecne = 2;
    public static int gameOverScene = 3;
    public static int row=3;
    public static int column=3;
    public static int rowColumnHeightWidth = 4;
    public static string gameSceneName = "Game";
    public static Vector3 initialCameraPosition;
    public static int totalCollisionCount;
    public static Vector3 wallOffset = new Vector3(0, -0.1f, 0);
    public static bool reverse;
    public static IEnumerator AsynchronousLoad(string scene, Image loadingImage, Text loadingText)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;
        while (!ao.isDone)
        {
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            loadingImage.rectTransform.sizeDelta = new Vector2(progress * Screen.width, 30);
            yield return 0.2f;
            if (progress == 1)
            {
                loadingText.text = "Done! Tap to continue";
                if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)
                {
                    ao.allowSceneActivation = true;
                    Util.startingTime = Time.time;
                }
            }
        }
    }
    public static int calculateScore(float value)
    {
        int cellCount = row * column;
        float multiplier = cellCount / value;
        float res = 100 * multiplier;
        return Mathf.FloorToInt(res + 0.5f);
    }
    public static int calculateScore()
    {
        float value = Time.time - startingTime;
        int cellCount = row * column;
        float multiplier = cellCount / value;
        float res = 100 * multiplier;
        return Mathf.FloorToInt(res + 0.5f);
    }
    public static string getScore(float value)
    {
        return calculateScore(value).ToString();
    }
    public static void addScore(int score)
    {
        loadScore();
        scores[3] = score;
        System.Array.Sort(scores);
        System.Array.Reverse(scores);
        PlayerPrefs.SetInt("s1", scores[0]);
        PlayerPrefs.SetInt("s2", scores[1]);
        PlayerPrefs.SetInt("s3", scores[2]);
    }
    public static void loadScore()
    {
        scores[0] = PlayerPrefs.GetInt("s1");
        scores[1] = PlayerPrefs.GetInt("s2");
        scores[2] = PlayerPrefs.GetInt("s3");
        scores[3] = 0;
    }
    public static string res()
    {
        loadScore();
        return "1. " + scores[0] + "\n" +
            "2. " + scores[1] + "\n" +
            "3. " + scores[2];
    }
    public static string currentTime()
    {
        string res;
        int timeDifference = (int)(Time.time - startingTime);
        res = (timeDifference / 60).ToString();
        res += " : ";
        res += (timeDifference % 60).ToString();
        return res;
    }
    public static void topView(GameObject camera,GameObject ball)
    {
        camera.transform.localEulerAngles = new Vector3(90, 0, 0);
        camera.transform.position = ball.transform.position + new Vector3(0, 5, 0);
    }
    public static Color[] powerUpColor = new Color[]
    {
        new Color(180,190,180),
        new Color(0,0,0),
        new Color(255,255,0),
        new Color(255,150,0),
        new Color(50,255,0),
        new Color(50,90,255),
        new Color(255,50,50),
    };
    public static IEnumerator doPowerUp(int code,GameObject ball)
    {
        resetPowerUp(ball);
        int mass;
        if (code == 1)
        {

            ball.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (code == 2)
        {

            ball.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if (code == 3)
        {
            Control.powerUpSpeedMod = 1.5f;
        }
        else if (code == 4)
        {
            Control.powerUpSpeedMod = 0.5f;
        }
        else if (code == 5)
        {
            mass = (int)ball.GetComponent<Rigidbody>().mass;
            ball.GetComponent<Rigidbody>().mass += mass / 2;
        }
        else if (code == 6)
        {
            mass = (int)ball.GetComponent<Rigidbody>().mass;
            ball.GetComponent<Rigidbody>().mass -= mass / 2;
        }
        else if(code==0)
        {
            Util.reverse = true;
        }
        yield return new WaitForSeconds(5);
        resetPowerUp(ball);
    }
    public static void resetPowerUp(GameObject ball)
    {
        ball.GetComponent<Rigidbody>().mass = 1000;
        ball.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        reverse = false;
        Control.powerUpSpeedMod = 1;
    }
    public static string[] powerUpNames = { "Jumbo", "Shrink", "Boost", "Slow Down", "Heavy", "Lite", "Mirror" };
}
