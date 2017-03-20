using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using System.Collections;

public class GameController : MonoBehaviour {

    enum State {
        Ready,
        Play,
        GameOver,
        Retry,
        Title,
    }

    State state;

    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private GameObject cloud;
    [SerializeField]
    private GameObject back_ground;
    [SerializeField]
    private GameObject manager_obj;
    [SerializeField]
    private EnemyManeger enemy_manager;
    [SerializeField]
    private ItemManeger item_manager;
    [SerializeField]
    private ScoreManeger score_manager;
    [SerializeField]
    private UIManeger ui_manager;
    [SerializeField]
    private SpeedManeger speedmanager;

    //UIのゲームオブジェクト
    [SerializeField]
    private GameObject button;
    [SerializeField]
    private GameObject retry_button;
    [SerializeField]
    private GameObject result;
    [SerializeField]
    private GameObject start_message;

    private SceneManeger scene_manager;
    private bool is_moveing_scene;

    //BGM,SE
    [SerializeField]
    private AudioClip main_bgm;
    [SerializeField]
    private AudioClip tap_se;

    //コンティニュー回数
    [SerializeField]
    private int retry_num;
    [SerializeField]
    private Text retry_num_text;

    private string game_id;

	// Use this for initialization
	void Start () {
#if UNITY_IPHONE
        game_is = "1048613";
#endif
#if UNITY_ANDROID
        game_id = "1048570";
#endif
#if UNITY_EDITOR
        game_id = "1048570";
#endif
        Advertisement.Initialize(game_id);
        scene_manager = GameObject.FindGameObjectWithTag("SceneControl").GetComponent<SceneManeger>();
        is_moveing_scene = false;
        Ready();	    
	}
	
	// Update is called once per frame
	void Update () {
        //ゲームのステートごとのｅｖｅｎｔ監視
        switch (state) {
            case State.Ready:
                //タッチしたら開始
                if (Input.GetMouseButton(0))
                    GameStart();
                break;

            case State.Play:
                if (!player.is_alive)
                    GameOver();
                break;

            case State.GameOver:
                if (player.Falling()) {
                    if (retry_num <= 0)
                        retry_button.SetActive(false);
                    button.SetActive(true);
                    retry_num_text.text = retry_num.ToString();
                }
                break;

            case State.Retry:
                if (player.Rising()){
                    //state = State.Ready;
                    state = State.Play;
                    Invoke("GameStart",1f);
                }
                break;
        }
	}

    void Ready() {
        state = State.Ready;
        player.SetIsActive(false);
        start_message.SetActive(true);
    }

    void GameStart() {
        state = State.Play;

        //TapStartの文字の無効化
        start_message.SetActive(false);
        //各オブジェクトの有効化
        player.SetIsActive(true);
        cloud.BroadcastMessage("SetIsActive", true);
        back_ground.BroadcastMessage("SetIsActive",true);
        enemy_manager.StartDeploy();
        item_manager.StartDeploy();
        //BGM
        SoundManeger.instance.SoundBGM(main_bgm);
        SoundManeger.instance.SoundSE(tap_se);
        //
        DataManager.instance.AddPlayNum();     
    }

    /// <summary>
    /// ステートをゲームオーバーにする。
    /// プレイヤーや敵の生成の活動をストップ
    /// スコアの保存
    /// いらないアセットの破棄
    /// </summary>
    void GameOver() {
        state = State.GameOver;
    
        player.SetIsActive(false);        
        cloud.BroadcastMessage("SetIsActive", false);
        back_ground.BroadcastMessage("SetIsActive", false);
        enemy_manager.StopDeploy();
        item_manager.StopDeploy();
        //ui_manager.PrintResult();
        result.SetActive(true);
        SoundManeger.instance.StopBGM();

        score_manager.Save();
        //キャッシュに残っていて使用していないアセットを破棄
        Resources.UnloadUnusedAssets();
        //キャッシュを削除
        Caching.CleanCache();
    }

    /// <summary>
    /// タイトルへ戻る
    /// </summary>
    public void ReturnTitle() {
        state = State.Title;
        if (!is_moveing_scene) {
            SoundManeger.instance.SoundSE(tap_se);
            button.SetActive(false);
            scene_manager.LoadScene(scene_manager.title);
            is_moveing_scene = true;
        }
    }

    /// <summary>
    /// 広告を見た後のリスタート
    /// 敵などを出現させたり、ＨＰの初期化等
    /// </summary>
    public void Retry() {        
        player.Init();
        if (retry_num > 0) {
            state = State.Retry;          
            SoundManeger.instance.SoundSE(tap_se);
            //ui_manager.DeleteResult();
            result.SetActive(false);
            button.SetActive(false);
            //下のコメント外すとスピード初期化でリトライ
            //speedmanager.Init();
            retry_num--;
            DataManager.instance.AddRetryNum();
        }
        /*
        player.SetIsActive(true);
        enemy_maneger.StartDeploy();
        item_maneger.StartDeploy();
        */
    }


    /*以下Unity Adsに関する追加コード*/
    public void ShowRewardedAd()
    {
        const string RewardedZoneId = "rewardedVideo";

        Debug.Log(Advertisement.IsReady(RewardedZoneId));
        Debug.Log(Advertisement.IsReady());
        if (Advertisement.IsReady(RewardedZoneId) && retry_num > 0)
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show(RewardedZoneId, options);
        }
    }


    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                Retry();
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }


    /*
    public void Show()
    {
        if (Advertisement.IsReady() && retry_num > 0)
        {
            Advertisement.Show(null, new ShowOptions
            {
                pause = true,
                resultCallback = result =>
                {
                    Debug.Log("広告閲覧終了。広告閲覧報酬を付与するならここで。");
                    Debug.Log(result.ToString());
                    Retry();
                }
            });
        }
    }
    */

}