using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleAnimation : MonoBehaviour {

    [SerializeField]
    private Image image;
    [SerializeField]
    private Sprite[] sprite;

    private int n;
    private float time;
    [SerializeField]
    private float change_time;

    void Start(){
        n = 0;
        time = 0;
        change_time -= 0.1f * DataManager.instance.GetSpeedCollectionNum();
    }

    // Update is called once per frame
    void Update() {
        if (time > change_time)
        {
            time = 0;
            n++;
            if (n >= sprite.Length)
                n = 0;
        }
        time += Time.deltaTime;
        image.sprite = sprite[n];

        // エスケープキー取得
        if (Input.GetKey(KeyCode.Escape))
        {
            // アプリケーション終了
            Application.Quit();
            return;
        }
    }
}
