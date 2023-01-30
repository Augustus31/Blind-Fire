using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoreScript : MonoBehaviour
{
    public GameController controllerScript;
    public TMPro.TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        controllerScript = GameObject.Find("GameController").GetComponent<GameController>();
        text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = controllerScript.score.ToString();
    }
}
