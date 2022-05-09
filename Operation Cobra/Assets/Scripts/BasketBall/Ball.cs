using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private Collider materialProps; //Used to adjust ball bounciness.
    private Vector3 thrownLocation; //Stores location where ball is thrown.
    private bool ranOnce;
    public bool _Thrown; //Determines whether the ball was recently thrown. 
    private bool inHand; //Determines whether the ball is held in the player's hand.
    private float timerPos = 0.25f;
    private float timer = 1f; //Timer which get decremented down to 0 after ball is thrown.
    private OVRInput.Button grabButton; //Used to check if button is pressed down.
    public OVRInput.Controller controller; //Used to get controller location, rotation and velocity.
    private Quaternion lastRotation, currentRotation;

    // Use this for initialization
    void Start () {
        materialProps = this.GetComponent<Collider>();
        ranOnce = false;
        _Thrown = false;

    }
	
	// Update is called once per frame
	void Update () {
        
        if (this.transform.parent == null)
        {

        }
        else if (this.transform.parent.name == "HandRight") //Checks if ball is held in right hand.
        {
            //Assigns the right controller to manipulate ball.
            grabButton = OVRInput.Button.SecondaryHandTrigger;
            controller = OVRInput.Controller.RTouch;
        }
        else if (this.transform.parent.name == "HandLeft") //Checks if ball is held in left hand.
        {
            //Assigns the left controller to manipulate ball.
            grabButton = OVRInput.Button.PrimaryHandTrigger;
            controller = OVRInput.Controller.LTouch;
        }

        if (!_Thrown & this.transform.parent != null) //Checks if ball is not thrown and is held in player's hand.
        {
            this.transform.position = new Vector3(this.transform.parent.position.x, this.transform.position.y, this.transform.parent.position.z); //Keeps ball under player's hand.
            if (ranOnce)
            {
                ToggleBounciness(1f); //Keeps ball bouncing under player's hand (gives the impression of dribbling the ball).
            }
            ranOnce = true;
            if (!_Thrown & this.transform.position.y >= (this.transform.parent.position.y - 0.05f)) //Checks if ball is not thrown and if above player hand.
            {
                this.transform.position = new Vector3(this.transform.parent.position.x, (this.transform.parent.position.y - 0.09f), this.transform.parent.position.z); //Keeps ball under player hand while dribbling.
            }

            if (!_Thrown & (this.transform.parent.parent.parent.localRotation.eulerAngles.x < 180f & this.transform.parent.parent.parent.localRotation.eulerAngles.x > 150f))
            {
                ReadyToThrow();
            }

            if (OVRInput.Get(grabButton))//Checks if the grab button is being held.
            {
                ReadyToThrow(); //Locks ball in hand.
            }

            else if (inHand & OVRInput.GetUp(grabButton)) //Checks if ball is locked in hand and button is released.
            {
                throwObject(); //The ball is released and thrown.
            }
        }

       if (this.transform.parent == null)//Checks if the ball is not tied to player's hand.
        {
            if (_Thrown) //Checks if the ball has been recently thrown.
            {
                timer -= Time.deltaTime; //Subtracts timer set after ball is thrown.
                if (timer <= 0f) //Checks if timer is down to 0.
                {
                    //The ball is marked as unthrown, making it ready to be picked up again.
                    _Thrown = false;
                    ToggleBounciness(0.95f);
                    timer = 1f;
                }
             //   Debug.Log(timer);
            }
        }
        else//Runs if the ball is in player's hand/ being dribbled.
        {
            if (ranOnce)
            {
                ranOnce = false;
                ToggleBounciness(0.95f);
            }
            lastRotation = currentRotation;
            currentRotation = this.transform.rotation;
        }

        if (this.transform.position.y <= -1)//Checks if ball falls under world.
        {
            this.transform.position = new Vector3(this.transform.position.x, 0.125f, this.transform.position.z);//Teleport ball back into playable space.
        }

        timerPos -= Time.deltaTime;
        if (timerPos <= 0f)
        {
            thrownLocation = this.transform.position;
            Debug.Log("This function was called!");
            timerPos = 0.25f;
        }
    }
    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Scorer") //Checks if ball entered blue net.
        {
            GameLoop._Score1 += other.GetComponent<Scorer>().score; //Adds score.
            Debug.Log(GameLoop._Score1);
            //other.enabled = false;
        }
        else if (other.gameObject.name == "Scorer2") //Checks if ball entered red net.
        {
            GameLoop._Score2 += other.GetComponent<Scorer>().score; //Adds score.
        }
    }

    void ToggleBounciness(float b)//Function to modify the bounciness of the ball.
    {
            materialProps.material.bounciness = b;      
    }

   void ReadyToThrow() //Function called when button is held and ball is ready to throw.
    {
        Debug.Log("Ready to throw");
        ToggleBounciness(0f); //Ball has its bounciness removed.
        inHand = true;
        this.GetComponent<Rigidbody>().isKinematic = true;//Disables physics for the ball.
        this.transform.position = new Vector3(this.transform.parent.position.x, (this.transform.parent.position.y - 0.09f), this.transform.parent.position.z);//Locks ball position in player hand.
    }

    void throwObject() //Function called when button is released; throwing the ball.
    {
        inHand = false;
        ToggleBounciness(0.5f);//Bounciness is restored to the ball.
        _Thrown = true;
        this.transform.parent = null;//Ball is detached from player hand.
        Rigidbody rigidbody = this.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;//Physics and collisions are restored to the ball.

        Transform trackingSpace = GameObject.Find("TrackingSpace").transform;
        rigidbody.velocity = trackingSpace.rotation * OVRInput.GetLocalControllerVelocity(controller); //Transfers velocity from controller to ball. Gives ball proper momentum when thrown.

        Vector3 throwVector = this.transform.position - (thrownLocation);//Gets direction where ball should be thrown.
        rigidbody.AddForce((throwVector) * 10, ForceMode.Force);//Adds extra force to ball when thrown.

        //rigidbody.angularVelocity = GetAngularVelocity();
        //Debug.Log("ThrownLocation: " + thrownLocation);
        //Debug.Log("This.transform position: " + this.transform.position);
        //Debug.Log("Throw Vector: " + throwVector);

        

        //rigidbody.velocity = GameObject.Find("HandRight").GetComponent<Rigidbody>().velocity;//
        //rigidbody.angularVelocity = OVRInput.GetLocalControllerAngularVelocity(controller);

        

        /* this.transform.LookAt(this.transform.position + this.transform.GetComponent<Rigidbody>().velocity);
         this.transform.forward = this.transform.parent.GetComponent<Rigidbody>().velocity;

         GetComponent<Rigidbody>().AddForce(GameObject.Find("CenterEyeAnchor").transform.forward * 1f);
         //this.transform.forward *= 2;*/
    }

    Vector3 GetAngularVelocity()//Function which return the velocity at which the ball is to be thrown. (Unused)
    {
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);
        return new Vector3(Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y), Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z));
    }
}
