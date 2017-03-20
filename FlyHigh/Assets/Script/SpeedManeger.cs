using UnityEngine;
using System.Collections;

/// <summary>
/// ゲームオブジェクトが動くスピードを管理するマネージャークラス
/// </summary>
public class SpeedManeger : MonoBehaviour {

    [SerializeField]
    private ScrollObject[] scroll_obj;
    [SerializeField]
    private GameObject back_ground;
    [SerializeField]
    private Item[] item;

    //一回で加算するスピード
    [SerializeField]
    private float add_speed;
    //初期スピード
    [SerializeField]
    private float start_speed;

    //[SerializeField]
    private float now_speed;

    [SerializeField]
    private ScoreManeger score_maneger;
    [SerializeField]
    private BackGroundManager back_grounds_manager;

    // Use this for initialization
    void Awake() {
        Init();
    }

    public void Init() {
        now_speed = start_speed;

    }

    /// <summary>
    /// スピードアップ関数
    /// </summary>
    public void SpeedUp() {
        if (now_speed >= 12)
            add_speed = 0.2f;
        if (now_speed >= 12.5)
            add_speed = 0.1f;
        now_speed += add_speed;

        score_maneger.SpeedUp();

        //背景のスピードアップ
        back_grounds_manager.SpeedUp();

        //雲のスピードを上げる
        for (int i = 0; i < scroll_obj.Length; i++)
            scroll_obj[i].speed += add_speed/2;

        //現在生成されているアイテムオブジェクトを見つけスピードを上げる
        GameObject[] item = GameObject.FindGameObjectsWithTag("Item");
        for (int i = 0; i < item.Length; i++) {
            item[i].GetComponent<Item>().AddSpeed(add_speed);
        }

        //現在生成されている敵オブジェクトを見つけスピードを上げる
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++) {
            enemies[i].GetComponent<Enemy>().AddSpeed(add_speed);
        }
    }

    /// <summary>
    /// 現在のスピードを取得
    /// </summary>
    /// <returns></returns>
    public float GetNowSpeed() {
        return now_speed;
    }
}