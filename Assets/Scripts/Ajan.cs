using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEditor;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class Ajan : Agent
{
    private Rigidbody rb;
    public Transform Hedef;


    public float carpan = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }



    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(Hedef.localPosition);
        sensor.AddObservation(transform.localPosition);

        sensor.AddObservation(rb.velocity.x);
        sensor.AddObservation(rb.velocity.z);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        Vector3 kontrolSinyali = Vector3.zero;
        kontrolSinyali.x = actions.ContinuousActions[0];
        kontrolSinyali.z = actions.ContinuousActions[1];

        rb.AddForce(kontrolSinyali * carpan);

        float hedefeOlanFark = Vector3.Distance(transform.localPosition, Hedef.localPosition);
        if (hedefeOlanFark < 1.5f)
        {
            SetReward(1.0f);
            EndEpisode();
        }

        if (transform.localPosition.y < 0f)
        {
            SetReward(-1f);
            EndEpisode();
        }


    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continiousActionsOut = actionsOut.ContinuousActions;
        continiousActionsOut[0] = Input.GetAxis("Horizontal");
        continiousActionsOut[1] = Input.GetAxis("Vertical");
    }




}
