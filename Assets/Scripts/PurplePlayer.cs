using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Purple players chase green players
public class PurplePlayer : Player
{
  // Our current target
  GreenPlayer target = null;
  // Have we managed to capture the target?
  bool captured = false;
  // The gaol we are taking the target to
  GameObject gaol = null;

  

//    bool followPurple = false;

  // Start overrides the baseclass Start, but uses it.
  protected override void Start()
  {
    base.Start();
    currentSpeed = 0.0f;
    gaol = gameManager.GetGaol();
  }

  

  // Update decides what to do, chase greens or bring them to gaol
  protected override void Update()
  {
    if (!captured)
      Chase();
    else
      TransportToGaol();
     
    // Use the Move method of the parent class
    base.Update();

        Debug.Log(captured);
//        ReturnToGame();
      //  Debug.Log(ReturnToGame());


    }

  // Locate a target, if needed, and follow it.
  private void Chase()
  {
    if (target == null)
    {
      target = gameManager.FindClosestTarget(this) as GreenPlayer;
    }

    Vector2 targetPosition = target.Position();
    Vector2 direction = targetPosition - this.position;
    float distance = direction.magnitude;
        

    float futureRotation = Mathf.Atan2(direction.y, direction.x);

    // Limit the rotation and linear speeds
    currentRotation += Mathf.Clamp(futureRotation - currentRotation, -maxRotationSpeed, maxRotationSpeed);
    currentSpeed = Mathf.Clamp(distance, 0.0f, maxSpeed);

       

       Debug.Log(distance + " and " + currentSpeed);

        //was: distance < currentSpeed
        if (distance == currentSpeed)
    {

            captured = true ;

          
        }

        Debug.Log("Chase");
        



    }

   
    // Take the prisoner to gaol and leave them there.  This method is still need to be modified.
  private void TransportToGaol()
  {
       
    Vector2 direction = new Vector2(gaol.transform.position.x, gaol.transform.position.z) - this.position;

        float distance = direction.magnitude;

        float futureRotation = Mathf.Atan2(direction.y, direction.x);
        currentRotation += Mathf.Clamp(futureRotation - currentRotation, -maxRotationSpeed, maxRotationSpeed);

        currentSpeed = Mathf.Clamp(distance, 0.0f, maxSpeed);

        if (distance < 3) // limiting Purple player movement, this will help to balance the game, giving two teams nearly same amount of possibility to win
        {
            captured = false;
        }



    }












}
