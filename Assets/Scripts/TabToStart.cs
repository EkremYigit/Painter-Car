using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabToStart : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public bool isStarted;

    // Update is called once per frame
    void Update()
    {
        if (isStarted) return;
        // Check if there is a touch
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButtonDown(0))
        {
            // Check if finger is over a UI element
            if (EventSystem.current.currentSelectedGameObject == null && !(GameObject.Find("HowToPlay") == null)
                                                                      && !FindObjectOfType<SettingsMenuController>()
                                                                          ._isExpanded)
            {
                StartGame();
                return;
            }

            try
            {
                if (!EventSystem.current.currentSelectedGameObject.CompareTag("UI"))
                {
                    Debug.Log("TAB TO START!!! -->>> TAG");
                    StartGame();
                }
            }
            catch (NullReferenceException ex)
            {
                //this avoid null point error.
                //  Debug.Log("DENIEDED");
            }
        }
    }

    private void StartGame()
    {
        isStarted = true;
        GameController.Instance.StartNavigation();
    }
}