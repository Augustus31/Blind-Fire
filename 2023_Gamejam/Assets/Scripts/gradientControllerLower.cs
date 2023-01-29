using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gradientControllerLower : MonoBehaviour
{
    public float skyHeight;
    public GameObject skyLine;
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        skyLine = GameObject.Find("SkyLine");
        skyHeight = skyLine.GetComponent<Transform>().position.y;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        skyHeight = skyLine.GetComponent<Transform>().position.y;
        if(skyHeight > 5.4)
        {
            skyHeight = 5.4f;
        }
        else if (skyHeight < -5.4)
        {
            skyHeight = -5.4f;
        }
        transform.localPosition = new Vector3(0, (5.4f - skyHeight) * -50f, 0);
        image.fillAmount = (skyHeight + 5.4f + ((5.4f - skyHeight) / 2)) / 10.8f;
    }
}
