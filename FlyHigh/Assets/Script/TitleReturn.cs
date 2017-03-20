using UnityEngine;
using System.Collections;

public class TitleReturn : MonoBehaviour {

    
    private bool scene_flag;
    private SceneManeger scene_manager;

	// Use this for initialization
	void Start () {
        scene_flag = true;
        scene_manager = GameObject.FindGameObjectWithTag("SceneControl").GetComponent<SceneManeger>();
    }

    // Update is called once per frame
    public void OnClick(){
        if (scene_flag)
        {
            scene_manager.LoadScene(scene_manager.title);
            scene_flag = false;
        }
    }
}
