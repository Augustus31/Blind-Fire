using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overlayController : MonoBehaviour
{
    public GameObject inputController;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        
        inputController.SetActive(true);
        gameObject.SetActive(false);
    }
}
