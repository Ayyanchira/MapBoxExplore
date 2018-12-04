using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using System;

public class TouchInputManager : MonoBehaviour {
    // Use this for initialization
    [SerializeField] GameObject MapboxMap;
    [SerializeField] Text uiText;
	void Start () {
		
	}
	
	// Update is called once per frame
	//void Update () {
    //    ray = Camera.main.ScreenPointToRay(Input.mousePosition); //I can't get touch.position to work
    //                                                             //Debug.DrawLine(ray.origin,ray.direction * 1000);

    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        if (Physics.Raycast(ray.direction, hit.normal, 1000))
    //        {
    //            if (hit.collider.gameObject.tag == "MapPoint")
    //            {
    //                Debug.Log("You just selected a point!!! ");
    //            }
    //        }
    //    }

    //    if (Input.GetButtonDown("Fire1"))
    //    {
    //        Debug.Log(Input.mousePosition);
    //    }


    //}

    void Update()
    {
        GetInputFromUser();
        GetMouseClickFromUser();
    }

    private void GetMouseClickFromUser()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "MapPoint")
            {
                Debug.Log("You just selected a point!!! ");
                print("Getting the Location script from gameobject...");
                Location location = hit.transform.gameObject.GetComponent<Location>();
                var latitude = location.latitude;
                var longitude = location.longitude;
                var selectedstate = location.state;
                print("Selected state is " + selectedstate + "\n\n");

                AbstractMap abstractMap = MapboxMap.GetComponent<AbstractMap>();
                Vector2d latlong = new Vector2d(latitude, longitude);
                //if (!abstractMap.isActiveAndEnabled)
                //{
                    abstractMap.Initialize(latlong, 13);
                //}

                //abstractMap.SetCenterLatitudeLongitude(latlong);
                abstractMap.UpdateMap();
                uiText.text = "Thats " + selectedstate;
            }
        }
    }

    private void GetInputFromUser()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.tag == "MapPoint")
            {
                Debug.Log("You just selected a point!!! ");
                print("Getting the Location script from gameobject...");
                Location location = hit.transform.gameObject.GetComponent<Location>();
                var latitude = location.latitude;
                var longitude = location.longitude;
                var selectedstate = location.state;
                print("Selected state is " + selectedstate + "\n\n");

                AbstractMap abstractMap = MapboxMap.GetComponent<AbstractMap>();
                Vector2d latlong = new Vector2d(latitude, longitude);
                abstractMap.Initialize(latlong, 13);
                //abstractMap.SetCenterLatitudeLongitude(latlong);
                abstractMap.UpdateMap();
                uiText.text = "Thats " + selectedstate;
            }
        }
    }
}

