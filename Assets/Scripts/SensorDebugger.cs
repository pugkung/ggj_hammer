using UnityEngine;
using UnityEngine.UI;

public class SensorDebugger : MonoBehaviour
{

    public GameObject outputArea;
    private Text outputTxt;

    // Start is called before the first frame update
    void Start()
    {
        outputTxt = outputArea.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        outputTxt.text = "X: " + Input.acceleration.x + "\n"
            + "Y: " + Input.acceleration.y + "\n"
            + "Z: " + Input.acceleration.z;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        /*
         * Observation Note 
         * 
         * We will use acceleration.x to detect swing motion
         * Range [0,-1] : tilt to right (left-handed swing)
         * Range [-1,0] : tilt to left (right-handed swing)
         * Solution : use Math.Abs(acceleration.x) to check instead (0: reset, 1:trigger)
         */
    }
}
