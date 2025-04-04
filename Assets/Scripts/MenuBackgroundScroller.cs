using UnityEngine;
using UnityEngine.UI;

public class MenuSroller : MonoBehaviour
{
    [SerializeField] 
    private RawImage _image;
    [SerializeField] 
    private float _x, _y;
    void Update()
    {
        _image.uvRect = new Rect(_image.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _image.uvRect.size);
    }
}
