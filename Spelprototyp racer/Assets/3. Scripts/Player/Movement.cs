//Movement script that handles the physics of the player.
//Hovering and thrust calculations are made here.
using UnityEngine;

public class Movement : MonoBehaviour {

    public float spd;

    //Here follows a set of settings that can be tweeked in the editor
    //How the player handles, how hovering works and some custom physics
    [Header("Drive Settings")]
    public float thrustForce = 17f;         //Forward thrust force
    public float noThrustFactor = .99f;     //Velocity percentage the player maintains if not thrusting.
    public float brakeFactor = .95f;        //-------------------||------------------- when applying brake.
    public float rollAngle = 30f;           //Angle the book leans when turning

    [Header("Hover Settings")]
    public float hoverHeight = 1.5f;
    public float groundDist = 5f;
    public float hoverForce = 300f;
    public LayerMask groundCheck;           //A layer mask that determines what layer ground is on
    public PIDSettings hoverPID;          //A PID controller that handles the smoothness of the hover effekt.

    [Header("Physics Settings")]
    public Transform shipBody;
    public float maxSpeed = 100f;
    public float onGroundGrav = 20f;
    public float inAirGrav = 60f;

    Rigidbody rigidbody;
    PlayerInput input;
    public float drag;
    bool isOnGround;

    
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();

        drag = thrustForce / maxSpeed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        spd = Vector3.Dot(rigidbody.velocity, transform.forward);

        CalculateHover();
        CalculatePropulsion();
	}

    void CalculateHover()
    {
        //Holds the normal of the ground
        Vector3 groundNormal;

        Ray ray = new Ray(transform.position, -transform.up);

        RaycastHit hitInfo;

        isOnGround = Physics.Raycast(ray, out hitInfo, groundDist, groundCheck);

        if (isOnGround)
        {
            //determine how high off the ground is
            float height = hitInfo.distance;

            //saves the normal of the ground
            groundNormal = hitInfo.normal.normalized;
            //use the PID settings to determine the required hover force
            float forcePercent = hoverPID.Seek(hoverHeight, height);

            //Calculates the total amount of hoverForce needed based on the grounds normal
            Vector3 force = groundNormal * hoverForce * forcePercent;
            //Calculate the force and direction of gravity.
            Vector3 gravity = -groundNormal * hoverForce * height;

            //Applies the hover and gravity forces
            rigidbody.AddForce(force, ForceMode.Acceleration);
            rigidbody.AddForce(gravity, ForceMode.Acceleration);
        }
        else
        {
            //If the player is not on the ground. Set normal to up. This prevents the player
            //from tipping over.
            groundNormal = Vector3.up;
            //Change the gravity applied to falling gravity.
            Vector3 gravity = -groundNormal * inAirGrav;
            rigidbody.AddForce(gravity, ForceMode.Acceleration);
        }

        //Calculate the pitch and roll of the player to match the orientetion of the ground.
        //This method uses projection.
        Vector3 projection = Vector3.ProjectOnPlane(transform.forward, groundNormal);
        Quaternion rotation = Quaternion.LookRotation(projection, groundNormal);

        //Move the ship to match the rotation of the ground. Uses lerp for smoothness.
        rigidbody.MoveRotation(Quaternion.Lerp(rigidbody.rotation, rotation, Time.deltaTime * 10f));

        //Cosmetic rotation. Makes the player mesh to bank based on the rotation it is allready in.
        float angle = rollAngle * -input.rudder;

        Quaternion bodyRotation = transform.rotation * Quaternion.Euler(0f, 0f, angle);

        shipBody.rotation = Quaternion.Lerp(shipBody.rotation, bodyRotation, Time.deltaTime * 10f);
    }

    void CalculatePropulsion()
    {
        float rotationForce = input.rudder - rigidbody.angularVelocity.y;
        rigidbody.AddRelativeTorque(0f, rotationForce, 0f, ForceMode.VelocityChange);

        float sideSpeed = Vector3.Dot(rigidbody.velocity, transform.right);

        //Friction calculation to the sides of the player. To add drift to the player
        //divide Time.fixedDeltaTime a bit.
        Vector3 sideFriction = -transform.right * (sideSpeed / Time.fixedDeltaTime);

        //Apply side friction
        rigidbody.AddForce(sideFriction, ForceMode.Acceleration);

        if(input.thruster <= 0f)
        {
            rigidbody.velocity *= noThrustFactor;
        }

        if(!isOnGround)
        {
            return;
        }

        float propulsion = thrustForce * input.thruster - drag * Mathf.Clamp(spd, 0f, maxSpeed);
        rigidbody.AddForce(transform.forward * propulsion, ForceMode.Acceleration);
    }

    void OnCollisionStay(Collision collision)
    {
        //Collision behaviour if colliding with a wall object
        if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            //Checks how much upward impulse is generated on collision nad pushes the player down
            //with equal amount of force to keep player on track.
            Vector3 upwardForceFromCollision = Vector3.Dot(collision.impulse, transform.up) * transform.up;
            rigidbody.AddForce(-upwardForceFromCollision, ForceMode.Impulse);
        }

    }

    public float GetSpeedPercentage()
    {
        return rigidbody.velocity.magnitude / maxSpeed;
    }
}
