using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleBackGround : MonoBehaviour {

    [SerializeField]
    private Image background_image;
    [SerializeField]
    private Sprite[] background_sp;


	// Use this for initialization
	void Start () {
        SetBackGround();
	}

    /// <summary>
    /// 背景を現在のハイスコアの背景にする
    /// </summary>
    void SetBackGround()
    {
        int count = 0;
        for (int i = 4; i <= 7; i++)
        {
            if (PlayerPrefs.GetInt("Collection" + i, 0) == 1)
                count++;
            else
                break;
        }
        background_image.sprite = background_sp[count];
    }

}
