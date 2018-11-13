using UnityEngine;
public class CF : MonoBehaviour
{
    public Transform target;
    public float distance = 5f;
    public float height = 3f;
    public float heightDamping = 2;
    void Awake()
    {
        Util.helpView = false;
        gameObject.GetComponent<Camera>().fieldOfView = 80;
    }
    void OnEnable()
    {
        Util.helpView = false;
        gameObject.GetComponent<Camera>().fieldOfView = 80;
    }
    void LateUpdate()
    {
        if (!target)
            return;
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, 0);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);
        transform.LookAt(target);
    }
}
