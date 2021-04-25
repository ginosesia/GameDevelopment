using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReplay : MonoBehaviour
{

    private List<ActionReplayRecord> records = new List<ActionReplayRecord>();
    private bool isInReplayMode;
    private Rigidbody rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isInReplayMode = !isInReplayMode;

            if (isInReplayMode)
            {
                SetTransform(0);
                rigidbody.isKinematic = true;
            }
            else
            {
                SetTransform(records.Count - 1);
                rigidbody.isKinematic = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isInReplayMode)
        {
            records.Add(new ActionReplayRecord
            {
                position = transform.position,
                rotation = transform.rotation
            });
        }
    }

    private void SetTransform(int index)
    {
        ActionReplayRecord actionReplayRecord = records[index];

        transform.position = actionReplayRecord.position;
        transform.rotation = actionReplayRecord.rotation;
    }
}
