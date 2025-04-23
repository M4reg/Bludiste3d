using UnityEngine;

public class Flashlight : MonoBehaviour
{

    public GameObject ON;
    public GameObject OFF;
    public bool isOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ON.SetActive(false);
        OFF.SetActive(true);
        isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isOn)
            {
                ON.SetActive(false);
                OFF.SetActive(true);
            }
            else
            {
                ON.SetActive(true);
                OFF.SetActive(false);
            }
            isOn = !isOn;
        }
    }
}
