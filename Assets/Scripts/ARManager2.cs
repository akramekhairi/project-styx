using JetBrains.Annotations;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using System.Collections.Generic;

public class ARManager2 : MonoBehaviour
{
    //This script is attached to an object called the AR manager were all the appropriate objects are hooked up to allow them to interact with each other

    private enum State
    {
        //Declaring what the different states indicate
        Idle = 0,
        FindingGround = 1,
        PickMesh = 2
    }

    //Variable declaration (public makes them accessible from the unity editor to allow us to hook up the assets we wish to change)
    public ARSessionOrigin sessionOrigin;
    public ARPointCloudManager pointCloudManager;
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;
    public GameObject asset;
    public ModelDialog dialog1;
    public ModelDialog dialog2;
    public ModelDialog dialog3;
    public ModelDialog dialog4;
    public ModelDialog dialog5;
    public ModelDialog dialog6;
    public ModelDialog dialog7;
    public ModelDialog dialog8;

    public GameObject crosshair;

    private Pose placementPoint;
    private State state = State.Idle;

    Vector3 screenCenter;
    List<ARRaycastHit> collisions = new List<ARRaycastHit>();

    private void Start()
    {
        //Start is called at the start of the runtime and sets the state to finding the ground to allow the user to specify where to place the flowchart
        state = State.FindingGround;
    }


    public void Update()
    {
        //Update is called every frame and when the user hasn't decided where to place the flowchart yet it lets them place the flowchart first then any touches after that are to find more details about the card
        if (state == State.FindingGround)
        {
            RaycastDetectAndPlace();
        }
        else if (state == State.PickMesh)
        {
            RayCastPickMesh();
        }
    }

    private void RaycastDetectAndPlace()
    {
        //Create a ray from the camera at the position where the user tapped to the space 
        screenCenter = sessionOrigin.camera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        //Check which plane or point cloud that ray collided with
        raycastManager.Raycast(screenCenter, collisions, TrackableType.All);



        //Store the collision in placementPoint
        placementPoint = collisions[0].pose;
        //Record the camera's position and store a normalizedPoint vector with the camera's coordinates except the y coordinate is 0 so on the floor
        Vector3 camForward = sessionOrigin.camera.transform.forward;
        Vector3 normalizedPoint = new Vector3(camForward.x, 0, camForward.z).normalized;
        //Setting the placementpoint's rotation 
        placementPoint.rotation = Quaternion.LookRotation(normalizedPoint);
        //Set the crosshair to placementPoint or where the camera is pointing to
        crosshair.transform.position = placementPoint.position;



        //if the user has tapped the screen
        if (Input.GetMouseButton(0))
        {
            crosshair.SetActive(false);
            //Move the flowchart to the position of surface which collided with the ray but place it 1 m higher so it could be in mid air 
            asset.transform.position = new Vector3(placementPoint.position.x, placementPoint.position.y + 0.5f, placementPoint.position.z + 0.30f);
            //Change the state to allow the user to tap on the objects
            state = State.PickMesh;

        }
    }

    private void RayCastPickMesh()
    {
        //If the user tapped on an object and there's no pop up on the screen
        if (Input.GetMouseButton(0))
        {
            //layerMask is used so only the objects we want the user to tap on can be interacted with
            RaycastHit raycastHit;
            LayerMask layerMask = LayerMask.GetMask(new string[] { "3D Model" });
            //Create a ray from the camera at the position where the user tapped to the space
            Ray ray = sessionOrigin.camera.ScreenPointToRay(Input.mousePosition);
            //Create a boolean that is true if the ray from the user's tap collides with an object within the layerMask
            bool collision = Physics.Raycast(ray, out raycastHit, Mathf.Infinity, layerMask);
            //If the user taps a valid object
            if (collision)
            {
                
                if (raycastHit.collider.name == "Card 1")
                {

                    dialog1.Set("The first patient in Wuhan City, China reports symptoms similar to a coronavirus infection. The case was identified based on a retrospective review. The disease is thought to have emerged in the Huanan seafood market although doubts are later cast on that theory.");
                }
                else if (raycastHit.collider.name == "Card 2")
                {
                    dialog2.Set("China informs the World Health Organization (WHO) about a cluster of cases of pneumonia in Wuhan, Hubei Province. The virus is still not understood.");
                }
                else if (raycastHit.collider.name == "Card 3")
                {

                    dialog3.Set("The World Health Organization goes on to an emergency footing for dealing with a major disease outbreak. Huanan seafood market is shut down.");

                }
                else if (raycastHit.collider.name == "Card 4")
                {

                    dialog4.Set("The first death from the new coronavirus is reported by Chinese state media. They say that a 61 - year - old man in Wuhan has died. He had underlying health conditions");


                }
                else if (raycastHit.collider.name == "Card 5")
                {

                    dialog5.Set("The United States announces its first confirmed case - a man in his 30s who had returned from a trip to Wuhan. There are also confirmed cases in Japan and South Korea.");

                }
                else if (raycastHit.collider.name == "Card 6")
                {
                    dialog6.Set("Wuhan is locked down by Chinese authorities in an attempt to halt the spread of the disease. Roads are severely restricted and rail and air services are suspended. Foreign governments begin to make plans to evacuate their citizens from the city, although this takes some time.");

                }
                else if (raycastHit.collider.name == "Card 7")
                {
                    dialog7.Set("The WHO reconvenes its organization’s Emergency Committee, two days after the first reports of limited humanto - human transmission outside China. The Director - General declares the outbreak a Public Health Emergency of International Concern.");
                }
                else if (raycastHit.collider.name == "Card 8")
                {

                    dialog8.Set("The Trump administration suspends entry into the United States by any foreign national who has traveled to China in the past 14 days, excluding immediate family members, permanent residents or American citizens.");

                }

            }
        }
    }


}