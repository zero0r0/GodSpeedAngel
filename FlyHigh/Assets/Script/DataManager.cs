using UnityEngine;
using System.Collections;

public class DataManager : MonoBehaviour {

    public static DataManager instance = null;

    const string MAX_HEIGHT_KEY = "MaxHeight";
    const string MAX_SPEED_KEY = "MaxSpeed";

    const string RETRY_NUM_KEY = "RetryNum";
    const string GET_HEAL_NUM = "GetHealNum";
    const string PLAY_NUM = "PlayNum";

    private int retry_num;
    private int get_heal_num;
    private int play_num;

    // Use this for initialization
    void Awake() {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        play_num = PlayerPrefs.GetInt(PLAY_NUM,0);
        retry_num = PlayerPrefs.GetInt(RETRY_NUM_KEY,0);
        get_heal_num = PlayerPrefs.GetInt(GET_HEAL_NUM,0);
        Debug.Log("playnum " + play_num);
        Debug.Log("retrynum " + retry_num);
        Debug.Log("healnum " + get_heal_num);

        DontDestroyOnLoad(this.gameObject);
        //PlayerPrefs.DeleteAll();
    }

    /// <summary>
    /// プレイ回数を１加算し、プレイ回数を保存する関数
    /// </summary>
    public void AddPlayNum(){
        play_num++;
        PlayerPrefs.SetInt(PLAY_NUM,play_num);
        if (play_num >= 30)
            SaveCollection(10);
        Debug.Log("playnum " + play_num);
    }

    public void AddRetryNum(){
        retry_num++;
        PlayerPrefs.SetInt(RETRY_NUM_KEY,retry_num);
        PlayerPrefs.Save();
        if (retry_num >= 15)
            SaveCollection(9);
        Debug.Log("retrynum " + retry_num);
    }

    public void AddGetHealNum(){
        get_heal_num++;
        PlayerPrefs.SetInt(GET_HEAL_NUM,get_heal_num);
        if (get_heal_num >= 100)
            SaveCollection(8);
        Debug.Log("healnum" + get_heal_num);
    }

    /// <summary>
    /// ハイスコアのセーブ
    /// </summary>
    /// <param name="score"></param>
    public void SaveMaxHeight(float score) {
        PlayerPrefs.SetFloat(MAX_HEIGHT_KEY, score);
        PlayerPrefs.Save();
    }

    

    /// <summary>
    /// ハイスピードの保存
    /// </summary>
    /// <param name="speed"></param>
    public void SaveMaxSpeed(float speed) {
        PlayerPrefs.SetFloat(MAX_SPEED_KEY, speed);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// ハイスコアのロード
    /// </summary>
    /// <returns></returns>
    public float LoadMaxHeight() {
        return PlayerPrefs.GetFloat(MAX_HEIGHT_KEY, 0);
    }

    /// <summary>
    /// ハイスピードのロード
    /// </summary>
    /// <returns></returns>
    public float LoadMaxSpeed() {
        return PlayerPrefs.GetFloat(MAX_SPEED_KEY, 0);
    }

    /// <summary>
    /// コレクションのセーブ
    /// </summary>
    /// <param name="collection_num"></param>
    public void SaveCollection(int collection_num)
    {
        PlayerPrefs.SetInt("Collection"+collection_num, 1);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// コレクション（スピード関連）の個数を取得
    /// </summary>
    /// <returns>スピードコレクションの個数</returns>
    public int GetSpeedCollectionNum()
    {
        int n = 0;
        for (int i = 0 ; i < 4; i++)
        {
            if (PlayerPrefs.GetInt("Collection" + i, 0) == 1)
                n++;
        }
        return n;
    }

}