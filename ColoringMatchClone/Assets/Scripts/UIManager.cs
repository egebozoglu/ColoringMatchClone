using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject statsArea;
    public Text accuracyText;

    public int imageIndex = 0;

    public Sprite[] spriteImages;

    public Image colorImage;

    public float RApple = 0.5529f;
    public float GApple = 0.7137f;
    public float BApple = 0f;
    public float AApple = 1f;

    public float RCar = 1f;
    public float GCar = 1f;
    public float BCar = 0f;
    public float ACar = 1f;

    public float RActual;
    public float GActual;
    public float BActual;
    public float AActual;

    public float percentage;

    public GameObject star1, star2, star3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"X: {(int)Input.mousePosition.x} ; Y:{(int)Input.mousePosition.y}");
        //if (Input.GetKey(KeyCode.Mouse0))
        //{
        //    CheckColor();
        //}
        colorImage.GetComponent<Image>().sprite = spriteImages[imageIndex];

    }

    public void NextImage()
    {
        if (imageIndex==1)
        {
            imageIndex = 0;
        }
        else
        {
            imageIndex = 1;
        }
    }

    public void PreviousImage()
    {
        if (imageIndex==0)
        {
            imageIndex = 1;
        }
        else
        {
            imageIndex = 0;
        }
    }

    public void PickRed()
    {
        BrushController.instance.pickedColor = 0;
    }

    public void PickGreen()
    {
        BrushController.instance.pickedColor = 1;
    }

    public void PickBlue()
    {
        BrushController.instance.pickedColor = 2;
    }

    public void PickYellow()
    {
        BrushController.instance.pickedColor = 3;
    }

    public void PickPurple()
    {
        BrushController.instance.pickedColor = 4;
    }

    public void CheckColor()
    {
        StartCoroutine(CheckColorCoroutine());
    }

    IEnumerator CheckColorCoroutine()
    {
        var tex = new Texture2D(Screen.width, Screen.height);
        yield return new WaitForEndOfFrame();
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();

        //var bla = tex.GetPixel(250, 250);

        var bla = tex.GetPixel(Screen.width/4 + 100, Screen.height/4);
        //print(bla);
        RActual = bla.r;
        GActual = bla.g;
        BActual = bla.b;
        AActual = bla.a;

        if (imageIndex == 0)
        {
            percentage = ApplePercentage();
        }
        else
        {
            percentage = CarPercentage();
        }

        statsArea.SetActive(true);
        accuracyText.text = "%" + percentage;

        if (percentage<35f)
        {
            star1.SetActive(true);
        }
        else if (percentage<75f)
        {
            star1.SetActive(true);
            star2.SetActive(true);
        }
        else
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);
        }

        foreach (GameObject brush in BrushController.instance.instantiatedBrushes)
        {
            Destroy(brush, 0f);
        }

        BrushController.instance.instantiatedBrushes.Clear();
    }

    float ApplePercentage()
    {
        var diffR = RApple - RActual;
        if (diffR<0)
        {
            diffR = diffR * -1f;
        }

        var diffG = GApple - GActual;
        if (diffG<0)
        {
            diffG = diffG * -1f;
        }

        var diffB = BApple - BActual;
        if (diffB<0)
        {
            diffB = diffB * -1f;
        }

        var diffA = AApple - AActual;
        if (diffA < 0)
        {
            diffA = diffA * -1f;
        }

        var percentageApple = (((1 - diffR) * 100) + ((1 - diffG) * 100) + ((1 - diffB) * 100) + ((1 - diffA) * 100)) / 4f;

        return percentageApple;
    }

    float CarPercentage()
    {
        var diffR = RCar - RActual;
        if (diffR < 0)
        {
            diffR = diffR * -1f;
        }

        var diffG = GCar - GActual;
        if (diffG < 0)
        {
            diffG = diffG * -1f;
        }

        var diffB = BCar - BActual;
        if (diffB < 0)
        {
            diffB = diffB * -1f;
        }

        var diffA = ACar - AActual;
        if (diffA < 0)
        {
            diffA = diffA * -1f;
        }

        var percentageCar = (((1 - diffR) * 100) + ((1 - diffG) * 100) + ((1 - diffB) * 100) + ((1 - diffA) * 100)) / 4f;

        return percentageCar;
    }

    public void OkClick()
    {
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);

        statsArea.SetActive(false);
    }
}
