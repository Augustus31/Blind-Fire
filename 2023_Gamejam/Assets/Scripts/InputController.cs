using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public bool disabled = false;
    public GameController gameController;
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
            worldPosition.x = Mathf.Clamp(worldPosition.x, -gameController.cwidth / 2, gameController.cwidth / 2);
            worldPosition.y = Mathf.Clamp(worldPosition.y, -gameController.cheight / 2, gameController.cheight / 2);
            this.transform.position = new Vector3(worldPosition.x, worldPosition.y, this.transform.position.z);
        }
    }
}
