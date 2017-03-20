using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// メインゲームシーンでのみのスコア処理を管理するクラス
/// </summary>
public class ScoreManeger : MonoBehaviour {

    public float score {
        get;
        private set;
    }
    public float speed {
        get;
        private set;
    }

    [SerializeField]
    private float[] collection_speed;

    private int max_wave;
    private int wave;
    [SerializeField]
    private float[] wave_score;

    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private SpeedManeger speed_manager;
    [SerializeField]
    private EnemyManeger enemy_manager;
    [SerializeField]
    private ItemManeger item_manager;
    [SerializeField]
    private BackGroundManager back_ground_manager;
    [SerializeField]
    private UIManeger ui_manager;

    //スピードアップ時スコアに反映させるスピードの加算値
    [SerializeField]
    private float add_score_speed;
    [SerializeField]
    private float start_score_speed;

    // Use this for initialization
    void Start() {
        Init();
    }

    // Update is called once per frame
    void Update() {
        if (player.is_active) {
            score += speed * Time.deltaTime;
            if (wave < max_wave && score >= wave_score[wave]) {
                enemy_manager.SetWaitTime(0.15f);
                wave++;
                back_ground_manager.SetWave(wave);
                enemy_manager.SetStage(wave);
                if (wave < 3)
                    enemy_manager.SetRandomRange(1);
            }
        }
    }

    void Init() {
        score = 0;
        wave = 0;
        max_wave = wave_score.Length;
        speed = start_score_speed;
    }


    public void SpeedUp() {
        speed += add_score_speed;
    }

    /// <summary>
    /// スコアに反映させるスピードの取得関数
    /// </summary>
    /// <returns></returns>
    public float GetNowSpeed()
    {
        return speed;
    }

    /// <summary>
    /// ハイスコア又はハイスピードが出た場合保存する
    /// スコア、スピードに応じて称号も保存する
    /// </summary>
    public void Save() {
        if (score > DataManager.instance.LoadMaxHeight()) {
            DataManager.instance.SaveMaxHeight(score);
            ui_manager.NewScore(score);
        }

        if (speed > DataManager.instance.LoadMaxSpeed()) {
            DataManager.instance.SaveMaxSpeed(speed);
            ui_manager.NewSpeed(speed);
        }

        for (int i = 0; i < collection_speed.Length; i++) {
            if (speed >= collection_speed[i])
                DataManager.instance.SaveCollection(i);
        }

        for(int i = collection_speed.Length; i < collection_speed.Length+max_wave; i++)
        {
            Debug.Log("loopcount" + i);

            if (score > wave_score[i - collection_speed.Length])
            {
                DataManager.instance.SaveCollection(i);
            }
        }
    }
    
}