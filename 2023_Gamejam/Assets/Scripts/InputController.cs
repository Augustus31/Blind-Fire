using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool disabled = false;
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
        if (!disabled)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePos);
            this.transform.position = new Vector3(worldPosition.x, worldPosition.y, this.transform.position.z);
        }
    }
}
