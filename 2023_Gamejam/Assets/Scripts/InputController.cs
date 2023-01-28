using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Camera mainCamera;

    // this contains the only sky line that should be on screen. Used for playerController
    public static GameObject singleton = null;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        if (singleton == null) {
            singleton = this.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePos);
        this.transform.position = new Vector3(0, worldPosition.y, 0);
    }
}
