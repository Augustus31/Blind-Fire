using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public PlayerMovement counter;

    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        counter = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        counter = GameObject.Find("Player").GetComponent<PlayerMovement>();
        textMeshPro.text = "Toggle Light: " + (counter.canToggle == true ? "Available": "On Cooldown");
    }
}
