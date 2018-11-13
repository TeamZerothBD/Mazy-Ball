using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelDetailsText : MonoBehaviour {
    public Text levelDetailsText;
    void Start()
    {
        Util.startingTime = Time.time;
    }
    void Update()
    {
        if (Util.type == 1)
        {
            float now = Time.time;
            int min = (int)((now - Util.startingTime) / 60);
            int sec = (int)((now - Util.startingTime) % 60);
            int miliSecDiffernce = (int)((now * 1000) % 1000);
            if (miliSecDiffernce.ToString().Length < 3)
            {
                throw new System.ArgumentOutOfRangeException();
            }
            else
            {
                levelDetailsText.text = min.ToString() + " : "
                    + sec.ToString() + " . "
                    + miliSecDiffernce.ToString().Substring(0, 3);
            }
        }
        else if (Util.type == 2) { }
    }
}
