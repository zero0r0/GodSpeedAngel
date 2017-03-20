using UnityEngine;
using System.Collections;

public class TitleButton : MonoBehaviour {

    [SerializeField]
    private GameController game_controller;

    public void OnClick() {
        game_controller.ReturnTitle();
    }
}
