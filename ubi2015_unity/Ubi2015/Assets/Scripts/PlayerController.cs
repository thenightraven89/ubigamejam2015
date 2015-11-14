using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 direction;

    public KeyCode keyUp;
    public KeyCode keyRight;
    public KeyCode keyDown;
    public KeyCode keyLeft;

    public GameObject trailObject;

    private float trailLife = 0.05f;
    private float ragaz = 2f;
    private float speed = 10f;
    private Transform t;
    private Vector3 initialPos;
    private Vector3 initialDir;
    public int life = 5;

    private Queue<GameObject> deadlyTrail;
    private Queue<GameObject> trail;

    private void Start()
    {
        t = transform;

        initialPos = t.position;
        initialDir = direction;

        deadlyTrail = new Queue<GameObject>();
        trail = new Queue<GameObject>();
        Respawn();
    }

    private void Respawn()
    {
        t.position = initialPos;
        direction = initialDir;

        StartCoroutine("AdvanceMovement");
        StartCoroutine("AdvanceDecay");
    }

    private void Die()
    {
        // destroy trail

        while (trail.Count > 0)
        {
            GameObject.Destroy(trail.Peek().gameObject);
            trail.Dequeue();
        }

        deadlyTrail.Clear();

        StopCoroutine("AdvanceMovement");
        StopCoroutine("AdvanceDecay");

        life = Mathf.Clamp(life - 1, 0, life);
        
        if (life == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            // respawn
            Respawn();
        }
    }

    private IEnumerator AdvanceDecay()
    {
        yield return new WaitForSeconds(ragaz);

        while (deadlyTrail.Count > 0)
        {
            yield return new WaitForSeconds(trailLife);
            deadlyTrail.Peek().GetComponentInChildren<Renderer>().material.color = Color.gray;
            deadlyTrail.Dequeue();
        }

        Die();
    }

    private IEnumerator AdvanceMovement()
    {
        var from = t.position;
        var to = from + direction;

        var newObject = Instantiate(trailObject, from, Quaternion.identity) as GameObject;
        deadlyTrail.Enqueue(newObject);
        trail.Enqueue(newObject);
        
        while (from != to)
        {
            from = Vector3.MoveTowards(from, to, speed * Time.deltaTime);
            t.position = from;
            yield return new WaitForEndOfFrame();
        }

        yield return StartCoroutine(AdvanceMovement());
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyUp) && direction != Vector3.back)
        {
            direction = Vector3.forward;
        }
        
        if (Input.GetKeyDown(keyRight) && direction != Vector3.left)
        {
            direction = Vector3.right;
        }

        if (Input.GetKeyDown(keyDown) && direction != Vector3.forward)
        {
            direction = Vector3.back;
        }

        if (Input.GetKeyDown(keyLeft) && direction != Vector3.right)
        {
            direction = Vector3.left;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Die();
        }

        // to not die in generated trail we do this:
        if (other.CompareTag("FreshTrail"))
        {
            other.tag = "Player";
        }
    }
}