using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    public float speed = 0.5f;
    private Image r;

    private void Awake()
    {
        r = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        r.material.mainTextureOffset += new Vector2(Time.deltaTime * speed, 0);
    }
}
