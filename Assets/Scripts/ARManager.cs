using JetBrains.Annotations;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using System.Collections.Generic;

public class ARManager : MonoBehaviour
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
    public ModelDialog dialog9;
    public ModelDialog dialog10;
    public ModelDialog dialog11;
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
            //Move the flowchart to the position of surface which collided with the ray but place it 0.5 m higher so it could be in mid air and 0.5 m forward 
            asset.transform.position = new Vector3(placementPoint.position.x, placementPoint.position.y+0.5f, placementPoint.position.z+0.50f) ;
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
            bool collision = Physics.Raycast(ray, out raycastHit, Mathf.Infinity,layerMask);
            //If the user taps a valid object
            if (collision)
            {
                //Set the cards' dialogs
                if (raycastHit.collider.name == "Card 1")
                {
                   
                    dialog1.Set("The mass extinction that happened around 250 million years ago, which eradicated nearly 95% of marine species, marked the end of the Permian period and the start of the Triassic period. With the sea no longer being the most friendly place to live, life started looking towards the land. Gradually more and more plants adapted to living on land, and animals soon followed. Among them, those who adapted most quickly and soon ruled the terrestrial world, were reptilians called Archosaurs, ancestors of “fearfully great lizards”, the dinosaurs. Through millions of years of evolution, dinosaurs became incredibly diverse, each species adapting to their own method of survival, but the most important decision they had to make was whether to become carnivores or herbivores.");
                }
                else if (raycastHit.collider.name == "Card 2")
                {
                    dialog2.Set("The early species of herbivores relied on their herd and their senses to defend from predators. Dryosaurus was such a dinosaur, who had no real way of defending itself, other than the fact that it had an above average intelligence for a dinosaur, which let it spot threats in time to escape. Though it was a rather fast animal, it had no way of outrunning most predator dinosaurs, but it was certainly faster than other herbivores which meant greater odds of survival.");
                }
                else if (raycastHit.collider.name == "Card 3")
                {
                   
                    dialog3.Set("Over time, prey dinosaurs developed defense mechanisms to ensure their survival. Therizinosaurus was a species that developed long,dangerous claws that were about 15 centimeters long, allowing it to fight off attacks, in addition to having the ability to move swiftly. This species was well adapted to its environment, as its long neck was an advantage in feeding off high vegetation, as well as spotting threats.");
                     
                }
                else if (raycastHit.collider.name == "Card 4")
                {
                    
                    dialog4.Set("One of the most recognizable dinosaurs was one that developed specifically with self defense in mind. Stegosaurus was slow and unintelligent compared to most other herbivores, but the unique feature of bone plates that ran along its spine on its back, along with a tail that ended with spikes capable of dealing a lethal blow, made this dinosaur a very much undesirable prey for many predators.");


                }
                else if (raycastHit.collider.name == "Card 5")
                {
                    
                    dialog5.Set("The mechanism of bone and skin outgrowths featured by stegosaurus was perfected in ankylosaurus, whose size and speed presented no challenge for any predator, but the difficulty to break through its armored back and head meant risking a lot of energy for a task that usually resulted in failure. A club-like tail meant that not only was hunting an ankylosaurus often unfruitful, it was also highly dangerous.");
                     
                }
                else if (raycastHit.collider.name == "Card 6")
                {
                    dialog6.Set("Herbivores had to try long and hard to come up with the most efficient way of staying alive. Some employed heightened intelligence, speed or defensive organs, while species of sauropods relied solely on massive size. They reached lengths of up to 45 meters, and weighed up to 80 metric tons. Even if they were very imobile, comically easy to spot and track and had no dangerous weapons like some other herbivores, sauropods easily defended against predators. Inflicting wounds enough to take down such a beast required a lot of work and a lot of time, during which predators were at risk of being hit by its enormous tails or stomped on by its front limbs.");
                     
                }
                else if (raycastHit.collider.name == "Card 7")
                {
                    dialog7.Set("Early carnivores were focused on preserving energy as much as possible and feeding on smaller, easier prey. Coelophysis was no taller than a human, and was only up to 3 meters long, but its small body and somewhat intelligent behaviour were enough to survive the harsh conditions of early terrestrial life.");
                }
                else if (raycastHit.collider.name == "Card 8")
                {

                    dialog8.Set("As life developed on land, animals grew larger and larger and soon predators had to find ways to take down bigger, more nutritious prey. Allosaurus was up to 3 meters tall and 10 meters long, and could reach high speeds for a short burst of time. It used its armored head to convert the momentum into striking force, knocking down its prey and sinking its numerous sharp teeth into it.");

                }
                else if (raycastHit.collider.name == "Card 9")
                {

                    dialog9.Set("In a dry, hot climate that defined the Jurassic period, having a sure supply of food was the only way to survive. This often meant specializing for a certain type of feeding, and Spinosaurus found its best odds of survival hunting aquatic animals. It developed fins and electroreception to make it as efficient as possible for a terrestrial animal to feed in water.");


                }
                else if (raycastHit.collider.name == "Card 10")
                {

                    dialog10.Set("High intelligence is a somewhat rare trait in the animal kingdom, as bigger brains required a lot of energy and therefore, a lot of food. Instead of developing massive bodies, the dromaeosaurus family used its intelligence for hunting and surviving. Unlike most carnivores, they moved in groups, a necessary adjustment considering that their meek size was no match for almost any dinosaur that lived at its time. They showed complex tactics in hunting, luring and isolating its prey, surrounding it with numbers and tiring it out. They featured dangerous claws that allowed them to hold on to the bodies of larger animals and cause heavy bleeding.");

                }
                else if (raycastHit.collider.name == "Card 11")
                {
                    dialog11.Set("Surely the most easily recognizable dinosaur, the Tyrannosaurus Rex, was the apex predator of the Jurassic ecosystems. With a massive body and the ability to reach extremely high speeds for its size, it feared no competition among other carnivores, and wasn’t picky when it came to what it could hunt. It had underdeveloped front limbs, but used its powerful jaw that had the bite force of 8000 pounds to strike lethal blows on its target, crushing any bones in its path. Its only real danger was starvation and dehydration, as moving such a body mass at such high speeds required incredible amounts of energy.");

                }

            }
        }
    }

    
}