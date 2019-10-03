using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
/// Rough Implementation of player movement.
/// Uses physics based movement to create "floaty" feeling.
/// Limits player velocity on x and y axes.
/// Allows player to Dash and Brake.
*/
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform cameraParent;
    [SerializeField] public float movementForce = 25f;
    [SerializeField] float maxSpeed_xy = 30f;
    [SerializeField] float maxSpeed_fall = -50f;
    [SerializeField] float fallSpeed = 2.5f;
    [SerializeField] float speedDecayRate = 25f;
    Vector3 movementDir;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            PlayerDash(true);
        if (Input.GetKeyUp(KeyCode.LeftShift))
            PlayerDash(false);

        if (Input.GetKeyDown(KeyCode.Space))
            PlayerBrake(true);
        if (Input.GetKeyUp(KeyCode.Space))
            PlayerBrake(false);

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");        
        movementDir.Set(moveHorizontal, 0f, moveVertical);
        movementDir.Normalize();
    }

    private void FixedUpdate()
    {
        rb.AddForce(movementDir * movementForce * Time.deltaTime, ForceMode.VelocityChange);

        if (rb.velocity.y > maxSpeed_fall)
            rb.AddForce(Physics.gravity * rb.mass * (fallSpeed - 1));
        else
        {
            // Cancel out gravity on rb
            rb.AddForce(-Physics.gravity * Time.deltaTime, ForceMode.VelocityChange);

            if (Mathf.Abs(rb.velocity.y - maxSpeed_fall) > 1f)
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + speedDecayRate * Time.deltaTime, rb.velocity.z);
        }
    }

    private void LateUpdate()
    {
        Vector3 angularVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

        if (Mathf.Abs(rb.velocity.y - maxSpeed_fall) < 1f)
            rb.velocity = new Vector3(rb.velocity.x, maxSpeed_fall, rb.velocity.z);
 
        if (angularVelocity.magnitude > maxSpeed_xy)
        {
            angularVelocity = angularVelocity.normalized * maxSpeed_xy;
            rb.velocity = new Vector3(angularVelocity.x, rb.velocity.y, angularVelocity.z);
        }

        //Debug.Log(rb.velocity.y);
    }

    private void PlayerDash(bool isDashing)
    {
        // Disable main cam to cause camera transition with CinemachineBrain
        cameraParent.transform.gameObject.SetActive(!isDashing);

        float dashSpeedMultiplier = 2f;
        fallSpeed = isDashing ? fallSpeed * 2 : fallSpeed / 2;
        maxSpeed_fall = isDashing ? maxSpeed_fall * dashSpeedMultiplier : maxSpeed_fall / dashSpeedMultiplier;
    }

    private void PlayerBrake(bool isBraking)
    {
        float brakeSpeedMultiplier = 2f;
        fallSpeed = isBraking ? fallSpeed / brakeSpeedMultiplier : fallSpeed * brakeSpeedMultiplier;
        maxSpeed_fall = isBraking ?  maxSpeed_fall / brakeSpeedMultiplier : maxSpeed_fall * brakeSpeedMultiplier;        
        speedDecayRate = isBraking ?  speedDecayRate * brakeSpeedMultiplier : speedDecayRate / brakeSpeedMultiplier;
    }
}
