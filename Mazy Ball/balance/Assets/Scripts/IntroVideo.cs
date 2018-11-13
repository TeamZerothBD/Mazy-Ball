using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
public class IntroVideo : MonoBehaviour {
    public RawImage rawImage;
    VideoPlayer introVideo;
    void Start()
    {
        //if (PlayerPrefs.GetInt("runSerial") == 0)
            //PlayerPrefs.SetInt("runSerial", PlayerPrefs.GetInt("runSerial") + 1);
        introVideo = gameObject.GetComponent<VideoPlayer>();
        introVideo.Prepare();
        rawImage.texture = introVideo.texture;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        StartCoroutine(playVideo());
    }
    IEnumerator playVideo()
    {
        while (!introVideo.isPrepared)
            yield return null;
        rawImage.texture = introVideo.texture;
        introVideo.Play();
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(Util.menuScene);
    }
}
