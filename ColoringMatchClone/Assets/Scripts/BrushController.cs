using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushController : MonoBehaviour
{
    public static BrushController instance;

    public GameObject tail;

    SpriteRenderer tailSpriteRenderer;

    public Sprite[] sprites;
    public Sprite[] touchSprites;

    public int pickedColor = 0; // 0 red, 1 green, 2 blue, 3 yellow, 4 purple

    public GameObject brushTouchPrefab;

    public List<GameObject> instantiatedBrushes = new List<GameObject>();

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        tailSpriteRenderer = tail.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        SetBrushColor();
        Painting();
    }

    public void SetBrushColor()
    {
        tailSpriteRenderer.sprite = sprites[pickedColor];
    }

    public void Painting()
    {
        GameObject brushTouch;
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                var touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                touchPosition.z = 0;
                var x = touchPosition.x;
                var y = touchPosition.y;
                if (x>-2.4f && x<2.4f)
                {
                    if (y>-4.5f && y<0f)
                    {
                        brushTouch = Instantiate(brushTouchPrefab, touchPosition, Quaternion.identity);
                        brushTouch.GetComponent<SpriteRenderer>().sprite = touchSprites[pickedColor];
                        transform.position = new Vector3(touchPosition.x + 1.25f, touchPosition.y - 2.4f, 0);
                        instantiatedBrushes.Add(brushTouch);
                    }
                }
            }
        }
    }
}
