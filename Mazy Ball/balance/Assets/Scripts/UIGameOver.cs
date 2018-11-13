using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIGameOver : MonoBehaviour {
    public Button gameOverOK;
    public RawImage SSImage;
    public GameObject SSImageObj;
    public Text resultText;
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        gameOverOK.onClick.AddListener(loadMenuScene);
        if (Util.win == true)
            SSImage.texture = GoalDetect.ssTexture;
        else
            SSImage.texture = null;
        float differenceTime = Util.endingTime - Util.startingTime;
        int min = (int)differenceTime / 60;
        int sec = (int)differenceTime % 60;
        if (Util.win == true)
        {
            SSImageObj.SetActive(true);
            resultText.text = "Congratulations!\n";
            resultText.text += "Time : \n" + min.ToString() + " : " + sec.ToString() + "\n" +
               "Score : \n" + Util.getScore(differenceTime).ToString();
            Util.addScore(Util.calculateScore(differenceTime));
        }
        else
        {
            SSImageObj.SetActive(false);
            resultText.text = "Failed!\n";
            resultText.text += "Time : \n" + min.ToString() + " : " + sec.ToString();
        }
    }
    void loadMenuScene()
    {
        SceneManager.LoadScene(Util.menuScene);
    }
}
