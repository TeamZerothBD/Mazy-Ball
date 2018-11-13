using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour {
    public GameObject menuPanel;
    public GameObject playPanel;
    public GameObject hScorePanel;
    public GameObject settingsPanel;
    public GameObject exitPanel;
    public GameObject loadingPanel;
    public Button playButton;
    public Button hScoreButton;
    public Button settingsButton;
    public Button exitButton;
    public Button playButtonBack;
    public Button hScoreButtonBack;
    public Button settingsButtonOk;
    public Button exitButtonBack;
    public Button timeTrialPlayButton;
    public Slider rowSlider;
    public Slider columnSlider;
    public Text rowText;
    public Text columnText;
    public Button exitButtonOk;
    public Image loadingImage;
    public Text loadingText;
    public Text HScores;
    void Start()
    {
        Util.loadScore();
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        rowSlider.minValue = 3;
        rowSlider.maxValue = 30;
        columnSlider.minValue = 3;
        columnSlider.maxValue = 30;
        rowSlider.value = PlayerPrefs.GetInt("rowCount");
        columnSlider.value = PlayerPrefs.GetInt("colCount");
        rowText.text = ((int)(rowSlider.value)).ToString();
        columnText.text = ((int)(columnSlider.value)).ToString();
        rowSlider.onValueChanged.AddListener(delegate { guiRowUpdate(); });
        columnSlider.onValueChanged.AddListener(delegate { guiColumnUpdate(); });
        backButtonClick();
        playButton.onClick.AddListener(playButtonClick);
        hScoreButton.onClick.AddListener(hScoreButtonClick);
        settingsButton.onClick.AddListener(settingsButtonClick);
        exitButton.onClick.AddListener(exitButtonClick);
        playButtonBack.onClick.AddListener(backButtonClick);
        hScoreButtonBack.onClick.AddListener(backButtonClick);
        settingsButtonOk.onClick.AddListener(settingsButtonOkClick);
        exitButtonBack.onClick.AddListener(backButtonClick);
        timeTrialPlayButton.onClick.AddListener(timeTrialPlayButtonClick);
        exitButtonOk.onClick.AddListener(exitButtonOkClick);
    }
    void playButtonClick()
    {
        menuPanel.SetActive(false);
        playPanel.SetActive(true);
        hScorePanel.SetActive(false);
        settingsPanel.SetActive(false);
        exitPanel.SetActive(false);
    }
    void hScoreButtonClick()
    {
        menuPanel.SetActive(false);
        playPanel.SetActive(false);
        hScorePanel.SetActive(true);
        settingsPanel.SetActive(false);
        exitPanel.SetActive(false);
        displayScores();
    }
    void displayScores()
    {
        HScores.text = Util.res();
    }
    void settingsButtonClick()
    {
        menuPanel.SetActive(false);
        playPanel.SetActive(false);
        hScorePanel.SetActive(false);
        settingsPanel.SetActive(true);
        exitPanel.SetActive(false);
    }
    void exitButtonClick()
    {
        menuPanel.SetActive(false);
        playPanel.SetActive(false);
        hScorePanel.SetActive(false);
        settingsPanel.SetActive(false);
        exitPanel.SetActive(true);
    }
    void backButtonClick()
    {
        menuPanel.SetActive(true);
        playPanel.SetActive(false);
        hScorePanel.SetActive(false);
        settingsPanel.SetActive(false);
        exitPanel.SetActive(false);
    }
    void updateRowColumn()
    {
        Util.row = (int)rowSlider.value;
        Util.column = (int)columnSlider.value;
    }
    void settingsButtonOkClick()
    {
        updateRowColumn();
        backButtonClick();
    }
    void timeTrialPlayButtonClick()
    {
        hideAll();
        loadingPanel.SetActive(true);
        StartCoroutine(Util.AsynchronousLoad(Util.gameSceneName,loadingImage,loadingText));
    }
    void exitButtonOkClick()
    {
        Application.Quit();
    }
    void guiRowUpdate()
    {
        rowText.text = ((int)(rowSlider.value)).ToString();
        PlayerPrefs.SetInt("rowCount", (int)(rowSlider.value));
    }
    void guiColumnUpdate()
    {
        columnText.text = ((int)(columnSlider.value)).ToString();
        PlayerPrefs.SetInt("colCount", (int)(columnSlider.value));
    }
    void hideAll()
    {
        menuPanel.SetActive(false);
        playPanel.SetActive(false);
        hScorePanel.SetActive(false);
        settingsPanel.SetActive(false);
        exitPanel.SetActive(false);
    }
}
