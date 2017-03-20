using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Secret : MonoBehaviour {

    [SerializeField]
    private GameObject secret_image_obj;
    //イラストが現在表示されているかどうか
    private bool is_active_image = false;

    public void OnClick()
    {
        if (!is_active_image)
        {
            secret_image_obj.SetActive(true);
            is_active_image = true;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && is_active_image)
        {
            secret_image_obj.SetActive(false);
            is_active_image = false;
        }
    }

}
