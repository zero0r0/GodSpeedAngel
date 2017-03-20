using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManeger : MonoBehaviour {

    [SerializeField]
    private ScoreManeger score_maneger;
    [SerializeField]
    private SpeedManeger speed_maneger;
    [SerializeField]
    private Text speed_text;
    private float speed;
    [SerializeField]
    private Text height_text;
    private float height;

    [SerializeField]
    private Text[] max_height_text;
    [SerializeField]
    private Text[] max_speed_text;
    private float max_height;
    private float max_speed;

    [SerializeField]
    private Text result_text;
    [SerializeField]
    private Text result_height;
    [SerializeField]
    private Text result_speed;

    void Start() {
        for (int i = 0; i < max_height_text.Length; i++){
            max_height_text[i].text = DataManager.instance.LoadMaxHeight().ToString("F0") + "m";
            max_speed_text[i].text = DataManager.instance.LoadMaxSpeed().ToString("F0") + "m/s";
        }
    }

	// Update is called once per frame
	void Update () {
        height = score_maneger.score;
        height_text.text = height.ToString("F0") + "m";
        speed_text.text = score_maneger.GetNowSpeed().ToString("F0") + " m/s";
        result_height.text = height_text.text;
        result_speed.text = speed_text.text;
	}

    //スピード更新時
    public void NewSpeed(float s)
    {
        max_speed_text[1].text = s.ToString("F0") + " m/s";
    }

    //スコア更新時
    public void NewScore(float h)
    {
        max_height_text[1].text = h.ToString("F0") + "m";
    }


    /*
    public void PrintResult() {
        result_text.text = "Result";
        for (int i = 0; i < result.Length; i++) {
            result[i].SetActive(true);
        }
        //result_height.text = height_text.text;
        //result_speed.text = speed_text.text;
        //button.SetActive(true);
    }

    public void DeleteResult() {
        result_text.text = "";
        for (int i = 0; i < result.Length; i++) {
            result[i].SetActive(false);
        }
        //result_height.text = "";
        //result_speed.text = "";
    }
     */

}