using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollectionManager : MonoBehaviour {

    public static CollectionManager instance = null;

    [SerializeField]
    private Text collection_name;
    [SerializeField]
    private Text collection_explain;

    [SerializeField]
    private Text collection_sum_text;

    //0~3:speed     4~7:high    8~11:
    [SerializeField]
    private GameObject[] collections;

    [SerializeField]
    private Sprite flame;

    [SerializeField]
    private GameObject secret_bottun;

    [SerializeField]
    private Sprite[] background_sp;
    [SerializeField]
    private Image background_image;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
        int collection_sum = 0;

        collection_explain.text = "";
        collection_name.text = "";

        

	    for(int i = 0; i < collections.Length-1; i++)
        {
            if (PlayerPrefs.GetInt("Collection" + i, 0) == 1)
            {
                //collections[i].SetActive(true);
                collection_sum++;
            } else {
                //collections[i].SetActive(false);
                collections[i].GetComponent<Collection>().SetFlame(flame);
            }
        }

        if (collection_sum == collections.Length-1){
            DataManager.instance.SaveCollection(collections.Length-1);
            collections[collections.Length - 1].GetComponent<Collection>().CollectionExplain += "\n\n秘密のパスワード：White Lilac";
            collection_sum++;
        }
        else{
            collections[collections.Length-1].GetComponent<Collection>().SetFlame(flame);
        }

        //コンプリート画面の表示のコード
        if (collection_sum == collections.Length){
            secret_bottun.SetActive(true);
        }

        collection_sum_text.text = collection_sum+" / "+collections.Length;

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

    public void SetCollection(string name, string explain){
        collection_name.text = name;
        collection_explain.text = explain;
    }
}
