using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class AnimationTrigger : MonoBehaviour
{
    [Header("Animator Specific")]
    public Animator animator;

    [Header("NPC Name")]
    public string talkToNode = "";

    [Header("Yarn Specific")]
    public DialogueRunner dialogueRunner; //refernce to the dialogue control
    public GameObject dialogueCanvas;
    private bool canvasActive;
    public YarnProject scriptToLoad;
    public Vector3 PostionSpeachBubble = new Vector3(0f, 2.0f, 0.0f);
    private float canvasTurnSpeed = 2;
    public GameObject playerGameObject;
    // Start is called before the first frame update

    private void Start()
    {
        dialogueCanvas = GameObject.FindGameObjectWithTag("Dialogue Canvas");
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        playerGameObject = GameObject.FindGameObjectWithTag("Player");


        if (scriptToLoad == null)
        {
            Debug.LogError("NPC3D not set up with yarn scriptToLoad ", this);
        }

        if (string.IsNullOrEmpty(talkToNode))
        {
            Debug.LogError("NPC3D not set up with talkToNode", this);
        }

        if (dialogueRunner == null)
        {
            Debug.LogError("dialogueRunner not set up", this);
        }

        if (dialogueCanvas == null)
        {
            Debug.LogError("Dialogue Canvas not set up", this);
        }

        if (playerGameObject == null)
        {
            Debug.LogError("Player Game Object not set up", this);
        }

        if (scriptToLoad != null && dialogueRunner != null && dialogueRunner != null)
        {
            dialogueRunner.yarnProject = scriptToLoad; //adds the script to the dialogue system
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (canvasActive)
        {
            Vector3 lookDir = dialogueCanvas.transform.position - playerGameObject.transform.position;
            float radians = Mathf.Atan2(lookDir.x, lookDir.z);
            float degrees = radians * Mathf.Rad2Deg;

            float str = Mathf.Min(canvasTurnSpeed * Time.deltaTime, 1);
            Quaternion targetRotation = Quaternion.Euler(0, degrees - 90, 0);
            dialogueCanvas.transform.rotation = Quaternion.Slerp(dialogueCanvas.transform.rotation, targetRotation, str);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!string.IsNullOrEmpty(talkToNode))
            {
                var isSad = animator.GetBool("isSad");
                animator.SetBool("isSad", false);
                Debug.Log("Collision Detected");
                if (dialogueCanvas != null)
                {
                    //move the Canvas to the object and off set
                    canvasActive = true;
                    dialogueCanvas.transform.SetParent(transform); // use the root to prevent scaling
                    dialogueCanvas.GetComponent<RectTransform>().anchoredPosition3D = transform.TransformVector(PostionSpeachBubble);
                }

                if (dialogueRunner.IsDialogueRunning)
                {
                    dialogueRunner.Stop();
                }
                Debug.Log("start dialogue");
                dialogueRunner.StartDialogue(talkToNode);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var isSad = animator.GetBool("isSad");
            animator.SetBool("isSad", true);
            Debug.Log("Collision Left");
            canvasActive = false;

        }
    }

   
}
