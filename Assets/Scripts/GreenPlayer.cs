using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// The green players get chased
public class GreenPlayer : Player
{

    PurplePlayer target = null;
    // The player should know if it has been captured, in which case it should follow its captor to gaol
    bool beenCaptured = false;  
    GameObject gaol = null;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        position = new Vector2(transform.position.x * Time.deltaTime, transform.position.z * Time.deltaTime);
        gaol = gameManager.GetGaol();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!beenCaptured)
            RandomMove();
        else
        {
            GoToJail();
            // GameManager.caught++;
        }

        base.Update();

    //    JailedGreen();
    }

    // Change direction by a bit and move forward
    private void RandomMove()
    {
        float offset = (Random.value - Random.value) / 3;

        currentRotation += (Mathf.Clamp(offset, 0.0f, maxRotationSpeed));

        if (target == null)
        {
            target = gameManager.FindClosestTarget(this) as PurplePlayer;
        }

        Vector2 targetPosition = target.Position();
        Vector2 direction = targetPosition - this.position;
        float distance = direction.magnitude;

        if (distance == 0)
        {
            beenCaptured = true;
        }
       

    }

    private void GoToJail()
    {

        Vector2 directionToJail = new Vector2(gaol.transform.position.x, gaol.transform.position.z) - this.position;

        float distanceToJail = directionToJail.magnitude;
       float futureRotation = Mathf.Atan2(directionToJail.y, directionToJail.x);
        currentRotation += Mathf.Clamp(futureRotation - currentRotation, -maxRotationSpeed, maxRotationSpeed);

        currentSpeed = Mathf.Clamp(distanceToJail, 0.0f, maxSpeed);
        //   Destroy(this.gameObject);




        if (distanceToJail < 2)
        {
           // float futureRotation = Mathf.Atan2(directionToJail.y, directionToJail.x);
            currentRotation += Mathf.Clamp(futureRotation - currentRotation, -maxRotationSpeed, maxRotationSpeed);

            currentSpeed = Mathf.Clamp(distanceToJail, 0.0f, maxSpeed);
        }
        else
        {
            currentSpeed = 0.0f;
            Destroy(this.gameObject);
            GameManager.caught++;

            Debug.Log("Caught: " + GameManager.caught);


        }
    }

        /*
         * Function for holding the prisoner in front of the goal and adding +1 into the Caught section
         * private void JailedGreen()
        {
            Vector2 directionToJail = new Vector2(gaol.transform.position.x, gaol.transform.position.z) - this.position;

            float distanceToJail = directionToJail.magnitude;
            if (distanceToJail == 2)
            {
                Destroy(this.gameObject);
                GameManager.caught++;

            }

        }
        */
    



}

    



