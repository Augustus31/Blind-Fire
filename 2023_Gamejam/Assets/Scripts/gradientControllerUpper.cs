using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gradientControllerUpper : MonoBehaviour
{
    public float skyHeight;
    public GameObject skyLine;
    public Image image;
    public float aspectRatio;

    private Image starImage;

    // Start is called before the first frame update
    void Start()
    {
        skyLine = GameObject.Find("SkyLine");
        skyHeight = skyLine.GetComponent<Transform>().position.y;
        image = GetComponent<Image>();
        starImage = GameObject.Find("StarBackground").GetComponent<Image>();
        aspectRatio = (Camera.main.aspect * 9f / 16f);
    }

    // Update is called once per frame
    void Update()
    {
        skyHeight = skyLine.GetComponent<Transform>().position.y;
        if (skyHeight > 5.4)
        {
            skyHeight = 5.4f;
        }
        else if (skyHeight < -5.4)
        {
            skyHeight = -5.4f;
        }
        transform.localPosition = new Vector3(0, ((-5.4f - skyHeight) * -50f), 0);
        image.fillAmount = (10.8f - (skyHeight + 5.4f) + ((5.4f - -1*skyHeight) / 2)) / 10.8f;

        float alpha = 1 - (skyHeight + 5.4f) / 10.8f;
        starImage.color = new Color(starImage.color.r, starImage.color.g, starImage.color.b, alpha);
    }
}
