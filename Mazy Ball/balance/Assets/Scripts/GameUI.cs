using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameUI : MonoBehaviour {
    public Button pausePlayButton;
    public Button backPlayButton;
    public Text pausePlayButtonText;
    public Text timeText;
    public Text scoreText;
    public GameObject playDetails;
    public GameObject backPanel;
    public Button backPanelOkButton;
    public Button backPanelCancelButton;
    public Button helpButton;
    public GameObject camera;
    public GameObject ball;
    void Start () {

        helpButton.onClick.AddListener(helpButtonAction);
        pausePlayButton.onClick.AddListener(pausePlayButtonAction);
        backPlayButton.onClick.AddListener(backPlayButtonAction);
        backPanelOkButton.onClick.AddListener(backPanelOkButtonAction);
        backPanelCancelButton.onClick.AddListener(backPanelCancelButtonAction);
        backPanel.SetActive(false);
	}
    void backPanelOkButtonAction()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Util.menuScene);
    }
    void backPanelCancelButtonAction()
    {
        backPanel.SetActive(false);
        playDetails.SetActive(true);
        Time.timeScale = 1;
    }
    void pausePlayButtonAction()
    {
        if(Time.timeScale==1)
        {
            Time.timeScale = 0;
            pausePlayButtonText.text = "Game\nPaused!";
        }
        else
        {
            Time.timeScale = 1;
            pausePlayButtonText.text = "Pause?";
        }
    }
    void backPlayButtonAction()
    {
        Time.timeScale = 0;
        backPanel.SetActive(true);
        playDetails.SetActive(false);
    }
    void Update()
    {
        timeText.text = "Time : " + Util.currentTime();
        scoreText.text = "Score : " + Util.calculateScore().ToString();
    }
    void helpButtonAction()
    {
        if (Util.helpView == true)
        {
            Util.helpView = false;
            Camera.main.fieldOfView = 80;
            camera.GetComponent<CF>().enabled = true;
            camera.GetComponent<CameraHandler>().enabled = false;
            ball.AddComponent<Rigidbody>();
            ball.GetComponent<Rigidbody>().mass = 1000;
            ball.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
        else 
        {
            if(Time.timeScale != 0)
            {

                Util.helpView = true;
                camera.GetComponent<CF>().enabled = false;
                camera.GetComponent<CameraHandler>().enabled = true;
                Util.topView(camera, ball);
                Destroy(ball.GetComponent<Rigidbody>());
            }
            else
            {
                Handheld.Vibrate();
            }
        }
    }
}
