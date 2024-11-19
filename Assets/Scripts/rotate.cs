using UnityEngine;

public class rotate : MonoBehaviour
{
    public float rotationSpeed = 0.1f;
    public RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float newRotation = rotationSpeed * Time.deltaTime + rectTransform.rotation.z;
        rectTransform.Rotate(0, 0, newRotation / 360);
    }
}
