using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Collection : MonoBehaviour {

    [SerializeField]
    private string collection_name;
    [SerializeField]
    private string collection_explain;
    public string CollectionExplain
    {
        get{
            return this.collection_explain;
        }
        set
        {
            this.collection_explain = value;
        }
    }
    private Image image;
    [SerializeField]
    private bool is_release = true;

    public void OnClick(){
        if (is_release)
            CollectionManager.instance.SetCollection(collection_name, collection_explain);
        else
            CollectionManager.instance.SetCollection("", collection_explain);
    }

    public void SetFlame(Sprite flame){
        is_release = false;
        image = GetComponent<Image>();
        image.sprite = flame;
    }
}
