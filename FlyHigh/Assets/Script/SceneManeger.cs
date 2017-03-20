using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneManeger : MonoBehaviour {

    public float wait_time;

    //private bool scene_flag;

    //フェイドイン、アウト用のイメージ画像
    public GameObject fade;

    public static SceneManeger instance = null;


    //シーン変数.enumとかにするべきだった‥
    public int title
    {
        get;
        private set;
    }
    public int main_game
    {
        get;
        private set;
    }
    public int gameover
    {
        get;
        private set;
    }
    private int next_scene;
    
    private Vector3 screenPos;
    private Vector3 worldPos;

    public bool is_retried = false;
    
    void Start(){
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        //scene_flag = true;
        title = 1;
        main_game = 2;
        gameover = 3;
        DontDestroyOnLoad(this.gameObject);
        LoadScene(title);
        is_retried = false;

        //PlayerPrefs.DeleteAll();
    }
	
	// Update is called once per frame
	void Update (){
        if (fade == null) {
            fade = GameObject.FindGameObjectWithTag("Fade");
        }
	}

    void OnApplicationQuit()
    {
        //キャッシュに残っていて使用していないアセットを破棄
        Resources.UnloadUnusedAssets();
        //キャッシュを削除
        Caching.CleanCache();
    }

    /// <summary>
    /// シーンの移行関数
    /// </summary>
    /// <param name="scene_num">
    /// シーン数(シーン名)
    /// </param>
    /// <returns></returns>
    public void LoadScene(int scene_num){
        //scene_flag = false;
        next_scene = scene_num;
        FadeIn();
        Invoke("ChangeScene", wait_time);
    }

    void ChangeScene() {
        FadeOut();
        SceneManager.LoadScene(next_scene);
        //scene_flag = true;
    }

    void FadeIn() {
        // SetValue()を毎フレーム呼び出して、１秒間に０から１までの値の中間値を渡す
        iTween.ValueTo(gameObject, iTween.Hash("from", 0f, "to", 1f, "time", 1f, "onupdate", "SetValue"));
    }

    void FadeOut() {
        iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f, "time", 1f, "onupdate", "SetValue"));
    }
    void SetValue(float alpha) {
        fade.GetComponent<Image>().color = new Color(255,255,255,alpha);
    }

}
