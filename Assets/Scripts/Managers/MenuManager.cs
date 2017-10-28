using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    private Canvas canvas;
    private void Awake()
    {

        canvas = GetComponent<Canvas>();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.enabled = !canvas.enabled;
            Pause();
        }
    }
    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        /*if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else {
            Time.timeScale = 0;
        */
    }
    public void Quit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
