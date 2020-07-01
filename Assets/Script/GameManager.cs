using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public enum Chapter
    {
        Begin,
        Play,
        PrepareEnd,
        End
    }

    public GameObject mainCamera;
    public GameObject Car;
    public GameObject UI;
    public EnemyManager enemyManager;
    public Transform StartPos;
    public Transform EndPos;
    public Chapter chapter;
    public VideoClip endMovie;

    private bool moviePlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        chapter = Chapter.Begin;
    }

    // Update is called once per frame
    void Update()
    {
        if(chapter == Chapter.Begin)
        {
            if (mainCamera.GetComponent<VideoPlayer>().isPlaying)
            {
                UI.SetActive(false);
                moviePlaying = true;
            }
            if (mainCamera.GetComponent<VideoPlayer>().isPlaying == false && moviePlaying)
            {
                moviePlaying = false;
                mainCamera.GetComponent<VideoPlayer>().enabled = false;
                UI.SetActive(true);
                Car.GetComponent<drive>().canDrive = true;
                enemyManager.startGenerateEnemy();
                mainCamera.GetComponent<VideoPlayer>().clip = endMovie;
                chapter = Chapter.Play;
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                mainCamera.GetComponent<VideoPlayer>().Stop();
            }

        }
        else if(chapter == Chapter.Play)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                Car.GetComponent<drive>().heal();
                Car.GetComponent<drive>().heal();
                Car.GetComponent<drive>().oilAmount = 100;
                Car.transform.position = StartPos.position;
                Car.transform.rotation = StartPos.rotation;
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                Car.transform.position = EndPos.position;
                Car.transform.rotation = EndPos.rotation;
            }
        }
        else if (chapter == Chapter.PrepareEnd)
        {
            UI.SetActive(false);
            mainCamera.GetComponent<VideoPlayer>().enabled = true;
            if (mainCamera.GetComponent<VideoPlayer>().isPrepared)
            {
                mainCamera.GetComponent<VideoPlayer>().Play();
                chapter = Chapter.End;
            }
        }


    }
}
