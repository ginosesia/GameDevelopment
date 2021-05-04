using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ActionReplay : MonoBehaviour
{

    private List<ActionReplayRecord> actionReplayRecords = new List<ActionReplayRecord>();
    private bool isInReplayMode;
    private Rigidbody2D rigidbody;
    private int currentIndex;

    [SerializeField] private Ball ball;
    [SerializeField] private Paddle paddle;
    [SerializeField] private Text replayIndicator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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
                SetModes(true);
            }
            else
            {
                SetTransform(actionReplayRecords.Count - 1);
                SetModes(false);
            }
        }
    }
    private void SetModes(bool state)
    {
        ball.inReplayMode = state;
        paddle.inReplayMode = state;
        rigidbody.isKinematic = state;
        replayIndicator.gameObject.SetActive(state);
    }

    private void FixedUpdate()
    {
        if (!isInReplayMode)
        {
            actionReplayRecords.Add(new ActionReplayRecord
            {
                position = transform.position,
            });
        }
        else
        {
            int nextIndex = currentIndex + 1;
            if (nextIndex < actionReplayRecords.Count)
            {
                SetTransform(nextIndex);
            } else if (nextIndex == actionReplayRecords.Count) {
                isInReplayMode = false;
                SetModes(false);
            }
        }
    }

    private void SetTransform(int index)
    {
        currentIndex = index;
        ActionReplayRecord actionReplayRecord = actionReplayRecords[index];
        transform.position = actionReplayRecord.position;
    }
}
