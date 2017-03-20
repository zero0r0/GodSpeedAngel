using UnityEngine;
using System.Collections;

public class RetryButton : MonoBehaviour
{

    [SerializeField]
    private GameController game_controller;

    private SceneManeger scene_manager;
    private int count = 0;

    void Start()
    {
        scene_manager = GameObject.FindGameObjectWithTag("SceneControl").GetComponent<SceneManeger>();
    }

    public void OnClick()
    {
        if (count < 2)
        {
            game_controller.Retry();
            count++;
        }
        else
        {
            if (scene_manager.is_retried)
            {
                game_controller.Retry();
            }
            else
            {
                game_controller.ShowRewardedAd();
                scene_manager.is_retried = true;
            }
        }
    }
}
 


        /*
        //game_controller.Retry();
        if (!is_retried){
            game_controller.Retry();
        else
            game_controller.ShowRewardedAd();

        is_retried = true;
        */
