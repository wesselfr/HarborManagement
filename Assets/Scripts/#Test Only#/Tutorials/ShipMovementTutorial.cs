using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipMovementTutorial : MonoBehaviour {

    [SerializeField]
    private GameObject _TutorialRoot;

    [SerializeField]
    private Text _TutorialText, _Index;

    [SerializeField]
    private Button _Yes, _No;

    private int step = 1;

    private int _WaypointCounter = 0;

	// Use this for initialization
	void Start () {
        _TutorialText.text = "Welcome captain. Would you like to know how to move your ships?";

        WaypointSetter.OnNewWaypoint += CountWaypoints;
	}
	
    public void Next()
    {
        step++;
    }
    
	// Update is called once per frame
	void Update () {

        switch (step)
        {
            case 2:
                _TutorialText.text = "Allright, lets get started. Click on a ship to continue. (You can quit the tutorial by pressing the escape key)";
                if (_WaypointCounter > 0)
                {
                    _WaypointCounter = 0;
                    Next();
                }
                _Yes.gameObject.SetActive(false);
                _No.gameObject.SetActive(false);
                break;
            case 3:
                _TutorialText.text = "Nice! Now click somewhere else to move your schip. Repeat this a few times";
                if (_WaypointCounter > 3)
                {
                    _WaypointCounter = 0;
                    Next();
                }
                break;
            case 4:
                _TutorialText.text = "You can deselect a ship by clicking or pressing the spacebar";

                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                { 
                    Next();
                }
                
                break;
            case 5:
                _TutorialText.text = "You can also select multiple ships by holding shift and your left mouse button down. Start dragging";
                if (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButtonDown(0))
                {
                    Next();
                }
                
                break;
            case 6:
                _TutorialText.text = "Select both ships and move both ships around a few times";
                if (_WaypointCounter > 5)
                {
                    _WaypointCounter = 0;
                    Next();
                }
                break;
            case 7:
                _TutorialText.text = "Well done Captain! Click to exit";
                if (Input.GetMouseButtonDown(0))
                {
                    StopTutorial();
                }
                break;


        }

        updateIndex();


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopTutorial();
        }
	}

    void CountWaypoints(Vector3 waypoint)
    {
        _WaypointCounter++;
    }

    void updateIndex()
    {
        _Index.text = step + "/12";
    }
    
    public void StopTutorial()
    {
        _TutorialRoot.SetActive(false);
    }

    
}
