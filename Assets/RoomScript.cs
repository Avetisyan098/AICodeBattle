using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class RoomScript : MonoBehaviour
{
    public Light RoomLight;
    public GameObject PoliceCar;
    public Transform hourHand;
    public Transform minuteHand;
    public Transform secondHand;
    public static int thiefNumber;
    public bool Museum = false;
    public string[] characteristics = {"1", "2", "3"};

    private void Awake() 
    {
        thiefNumber = UnityEngine.Random.Range(0,3);
    }

    private void Update()
    {
        if (!Museum)
        {
            DateTime currentTime = System.DateTime.Now;

            int hours = currentTime.Hour % 12;
            int minutes = currentTime.Minute;
            int seconds = currentTime.Second;

            float hourRotation = hours * 30f;
            float minuteRotation = minutes * 6f;
            float secondRotation = seconds * 6f;

            hourHand.localEulerAngles = new Vector3(0f, 0f, hourRotation);
            minuteHand.localEulerAngles = new Vector3(0f, 0f, minuteRotation);
            secondHand.localEulerAngles = new Vector3(0f, 0f, secondRotation);
        }
        if (Input.GetKey(KeyCode.H))
        {
            Animate();
        }
        Debug.Log(thiefNumber);
    }

    public void Animate() 
    {
        Instantiate(PoliceCar, new Vector3(0, 0, 0), Quaternion.identity);
        StartCoroutine("Animation");
    }

    private IEnumerator Animation()
    {
        yield return new WaitForSeconds(2f);
        for (float i = 1; i < 10; i++) 
        {
            yield return new WaitForSeconds(0.1f);
            RoomLight.intensity = 5f - i * 0.5f;
        }
        yield return new WaitForSeconds(1f);
        DontDestroyOnLoad(gameObject);
        Museum = true;
        SceneManager.LoadScene(1);
    }

    public string getCharacteristics() 
    {
        return characteristics[thiefNumber];
    }
}

