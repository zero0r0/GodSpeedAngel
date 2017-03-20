using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultManeger : MonoBehaviour {

    [SerializeField]
    private Text height_text;
    [SerializeField]
    private Text speed_text;
    [SerializeField]
    private Text max_height_text;
    [SerializeField]
    private Text max_speed_text;

    private int speed;
    private int score;
    private int max_height;
    private int max_speed;

    private ScoreManeger score_maneger;

    const string HIGH_SCORE_KEY = "HighScore";
    const string HIGH_SPEED_KEY = "HighSpeed";

	// Use this for initialization
	void Start () {
        score_maneger = GameObject.FindGameObjectWithTag("ScoreManeger").GetComponent<ScoreManeger>();
        score = (int)score_maneger.score;
        speed = (int)score_maneger.speed*10;

        height_text.text = score.ToString();
        speed_text.text = speed.ToString();

        if (score > LoadMaxHeight()) {
            SaveHighScore(score);
            max_height = score;
        } else
            max_height = LoadMaxHeight();

        if (speed > LoadMaxSpeed()) {
            SaveHighSpeed(speed);
            max_speed = speed;
        } else
            max_speed = LoadMaxSpeed();

        max_height_text.text = max_height.ToString();
        max_speed_text.text = max_speed.ToString();
	}

    /// <summary>
    /// ハイスコアのセーブ
    /// </summary>
    /// <param name="score"></param>
    void SaveHighScore(int score) {
        PlayerPrefs.SetInt(HIGH_SCORE_KEY, score);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// ハイスピードの保存
    /// </summary>
    /// <param name="speed"></param>
    void SaveHighSpeed(int speed) {
        PlayerPrefs.SetInt(HIGH_SPEED_KEY, speed);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// ハイスコアのロード
    /// </summary>
    /// <returns></returns>
    int LoadMaxHeight() {
        return PlayerPrefs.GetInt(HIGH_SCORE_KEY, -1);
    }

    /// <summary>
    /// ハイスピードのロード
    /// </summary>
    /// <returns></returns>
    int LoadMaxSpeed() {
        return PlayerPrefs.GetInt(HIGH_SPEED_KEY, -1);
    }

}
