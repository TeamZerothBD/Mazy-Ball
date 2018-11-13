using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Control : MonoBehaviour {
    MeshRenderer ballMeshRenderer;
    Rigidbody thisRigid;
    public static float speedModifier = 0.3f;
    float jumpModifier = 0.2f;
    public static float andMul = 8500;
    public static float powerUpSpeedMod = 1;
    bool touched = true;
    float fb;
    float lr;
    float moveH;
    float moveV;
    bool isDesktop;
    Gyroscope m_Gyro;
    public Text timerText;
    Coroutine oldCoroutine;
    void Start () {



        Util.helpView = false;
        andMul = 7500;
        speedModifier = 1f;
        Util.reverse = false;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        ballMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        transform.position = Util.startingPosition;
        thisRigid = this.GetComponent<Rigidbody>();
        if (SystemInfo.deviceType == DeviceType.Desktop)
            isDesktop = true;
        else
        {
            isDesktop = false;
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        m_Gyro = Input.gyro;
        m_Gyro.enabled = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.GetComponent<PowerUp>() != null)
        {
            int powerUpCode = collision.collider.gameObject.GetComponent<PowerUp>().powerUpCode;
            Destroy(collision.collider.gameObject);
            if(oldCoroutine!=null)
            {
                StopCoroutine(oldCoroutine);
            }
            StartCoroutine(Util.doPowerUp(powerUpCode,gameObject));
            oldCoroutine = StartCoroutine(countDown(powerUpCode));
        }
        touched = true;
        if (collision.collider.tag != "floor")
        {
            thisRigid.velocity = thisRigid.velocity / 2;
        }
        else
        {
            touched = true;
            ballMeshRenderer.material.color =
                collision.collider.GetComponent<MeshRenderer>().material.color;
        }
    }
    void OnCollisionStay(Collision collision)
    {
        touched = true;
    }
    void OnCollisionExit(Collision collision)
    {
        touched = false;    
    }

    void FixedUpdate ()
    {
        if (Util.helpView == false)
        {
            thisRigid = gameObject.GetComponent<Rigidbody>();
            if (isDesktop == true)
            {
                lr = Input.GetAxis("Vertical");
                fb = Input.GetAxis("Horizontal");
                if (Input.GetKeyDown(KeyCode.Space) && touched)
                    thisRigid.velocity += new Vector3(0, 1, 0) *
                        jumpModifier * thisRigid.velocity.magnitude * 0.01f;
                if (Util.reverse == false)
                    thisRigid.velocity += new Vector3(fb, 0, lr) * speedModifier * powerUpSpeedMod;
                else
                    thisRigid.velocity += new Vector3(fb, 0, lr) * -speedModifier * powerUpSpeedMod;
            }
            else
            {
                if (touched == true)
                {
                    moveH = Input.acceleration.x;
                    moveV = Input.acceleration.y;
                    Vector3 movement;
                    if (Util.reverse == false)
                        movement = new Vector3(moveH, 0.0f, moveV) * andMul * powerUpSpeedMod;
                    else
                        movement = new Vector3(moveH, 0.0f, moveV) * -andMul * powerUpSpeedMod;
                    thisRigid.AddForce(movement);
                }
            }
        }

    }

    public IEnumerator countDown(int code)
    {
        for(int i=50;i>=0;i--)
        {
            timerText.text = Util.powerUpNames[code] + "\n" +
                "Time Left : " + (i / 10).ToString() + "." + (i % 10).ToString();
            yield return new WaitForSeconds(0.1f);
        }
        timerText.text = "Power Up Removed";
        yield return new WaitForSeconds(0.5f);
        timerText.text = "";
    }
    
}
