using UnityEngine;
using System.Collections;

public class MoveSceneButton : MonoBehaviour
{
    private bool scene_flag;

    //次に移るシーン番号
    [SerializeField]
    private int scene_num;

    [SerializeField]
    private GameObject Button;

    // Use this for initialization
    void Start()
    {
        scene_flag = true;
    }

    // Update is called once per frame
    public void OnClick()
    {
        if (scene_flag)
        {
            SceneManeger.instance.LoadScene(scene_num);
            Button.SetActive(false);
            scene_flag = false;
        }
    }
}
