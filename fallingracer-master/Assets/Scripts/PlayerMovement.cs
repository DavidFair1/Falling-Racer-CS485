using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Rough Implementation of player movement.
/// Uses physics based movement to create "floaty" feeling.
/// Limits player velocity on x and y axes.
/// Allows player to Dash and Brake.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform cameraParent;
    [SerializeField] GameObject playerModelFemale;
    [SerializeField] GameObject playerModelMale;
    [SerializeField] GameObject parachute;

    [SerializeField] public float movementForce = 25f;
    [SerializeField] float maxSpeed_xy = 30f;
    [SerializeField] float maxSpeed_fall = 50f;
    [SerializeField] float fallSpeedMultiplier = 2.5f;
    [SerializeField] float fallSpeedDecayRate = 25f;
    Rigidbody rb;
    Transform player;
    Vector3 movementDir;
    bool parachuteOpen = false;

    public static UnityEvent playerDestroyedEvent = new UnityEvent();

    #region Unity Callbacks
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = this.transform;

        switch(PlayerPrefs.GetFloat("Character Selected", -999))
        {
            case 0:
                playerModelFemale.SetActive(true);
                playerModelMale.SetActive(false);
                break;
            case 1:
                playerModelFemale.SetActive(false);
                playerModelMale.SetActive(true);
                break;
            case -999:
                Debug.Log("Default value returned for character selection");
                break;
            default:
                Debug.Log("ERROR: Invalid value returned for character selection");
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            PlayerDash(true);
        if (Input.GetKeyUp(KeyCode.LeftShift))
            PlayerDash(false);

        /* Player Braking
        if (Input.GetKeyDown(KeyCode.Space))
            PlayerBrake(true);
        if (Input.GetKeyUp(KeyCode.Space))
            PlayerBrake(false);
            */

        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(OpenParachute());

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");      
        movementDir.Set(moveHorizontal, 0f, moveVertical);
        movementDir.Normalize();       
    }

    private void FixedUpdate()
    {
        rb.AddForce(movementDir * movementForce * Time.deltaTime, ForceMode.VelocityChange);

        ClampPlayerFallSpeed();
        
        RotatePlayerHorizontally(Input.GetAxis("Horizontal"));
    }

    private void LateUpdate()
    {
        Vector3 angularVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        // Clamp player's vert + hor velocity below max speed
        if (angularVelocity.magnitude > maxSpeed_xy)
        {
            angularVelocity = angularVelocity.normalized * maxSpeed_xy;
            rb.velocity = new Vector3(angularVelocity.x, rb.velocity.y, angularVelocity.z);
        }
    }
    #endregion

    #region Movement
    private void PlayerDash(bool isDashing)
    {
        if (parachuteOpen)
            return;

        // Disable main cam to cause camera transition with CinemachineBrain
        cameraParent.transform.gameObject.SetActive(!isDashing);

        float dashSpeedMultiplier = 2f;
        fallSpeedMultiplier = isDashing ? fallSpeedMultiplier * 2 : fallSpeedMultiplier / 2;
        maxSpeed_fall = isDashing ? maxSpeed_fall * dashSpeedMultiplier : maxSpeed_fall / dashSpeedMultiplier;
    }

    private IEnumerator OpenParachute()
    {
        if (parachuteOpen)
            yield break;

        parachuteOpen = true;
        parachute.SetActive(true);
        yield return new WaitForSeconds(1f);

        movementForce = 20f;
        fallSpeedMultiplier = 1.25f;
        maxSpeed_fall = 20f;   
    }

    /*
    private void PlayerBrake(bool isBraking)
    {
        float brakeSpeedMultiplier = 2f;
        fallSpeedMultiplier = isBraking ? fallSpeedMultiplier / brakeSpeedMultiplier : fallSpeedMultiplier * brakeSpeedMultiplier;
        maxSpeed_fall = isBraking ?  maxSpeed_fall / brakeSpeedMultiplier : maxSpeed_fall * brakeSpeedMultiplier;        
        fallSpeedDecayRate = isBraking ?  fallSpeedDecayRate * brakeSpeedMultiplier : fallSpeedDecayRate / brakeSpeedMultiplier;
    }
    */

    private void RotatePlayerHorizontally(float horizontalInput)
    {
        float rotation = parachuteOpen ? 3f : 20f;
        float newAngle = Mathf.LerpAngle(player.rotation.eulerAngles.z, rotation * -horizontalInput, Time.time);
        transform.eulerAngles = new Vector3(0, 0, newAngle);

    }

    /// <summary>
    /// If player is falling too fast, gradually reduce their fall speed
    /// </summary>
    private void ClampPlayerFallSpeed()
    {
        if (rb.velocity.y > -maxSpeed_fall)
            rb.AddForce(Physics.gravity * rb.mass * (fallSpeedMultiplier - 1));
        else
        {
            // Cancel out gravity on rb
            rb.AddForce(-Physics.gravity * Time.deltaTime, ForceMode.VelocityChange);

            if (Mathf.Abs(rb.velocity.y + maxSpeed_fall) > 1f)
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + fallSpeedDecayRate * Time.deltaTime, rb.velocity.z);
            else
                rb.velocity = new Vector3(rb.velocity.x, -maxSpeed_fall, rb.velocity.z);
        }
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        switch(other.gameObject.tag)
        {
            case "Powerup":
                StartCoroutine(anvilPowerup());
                break;
            case "Bird":
                StartCoroutine(BirdEnemyHit());
                break;
            //default:
                //Debug.LogError(other.gameObject.tag);
                //break;
        }
    }
    private IEnumerator anvilPowerup()
    {
        fallSpeedMultiplier = fallSpeedMultiplier * 2;
        maxSpeed_fall = maxSpeed_fall * 2;

        yield return new WaitForSeconds(3f);

        fallSpeedMultiplier = fallSpeedMultiplier / 2;
        maxSpeed_fall = maxSpeed_fall / 2;
    }

    private IEnumerator BirdEnemyHit()
    {
        fallSpeedMultiplier = fallSpeedMultiplier / 2;
        maxSpeed_fall = maxSpeed_fall / 2;

        yield return new WaitForSeconds(3f);

        fallSpeedMultiplier = fallSpeedMultiplier * 2;
        maxSpeed_fall = maxSpeed_fall * 2;
    }

    private void OnDestroy()
    {
        playerDestroyedEvent.Invoke();
    }
}
