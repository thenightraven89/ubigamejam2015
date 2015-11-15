using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public Vector3 direction;

    public KeyCode keyUp;
    public KeyCode keyRight;
    public KeyCode keyDown;
    public KeyCode keyLeft;

    public GameObject trailObject;

    private float trailLife = 0.097f;
    private float ragaz = 1.75f;
    public float speed = 8f;
    private Transform t;
    private Vector3 initialPos;
    private Vector3 initialDir;
    public int life = 5;
    private Queue<GameObject> deadlyTrail;
    private Queue<GameObject> trail;
    public bool hasShiled;

    private void Start()
    {
        t = transform;
        initialPos = t.position;
        initialDir = direction;
        deadlyTrail = new Queue<GameObject>();
        trail = new Queue<GameObject>();

        Respawn(false);
    }

    private void Respawn(bool invul)
    {
        direction = initialDir;
        t.position = initialPos;
        t.rotation = GetRotationFromDirection(direction);
        isInvulnerable = invul;
        StartCoroutine("AdvanceMovement");
        StartCoroutine("AdvanceDecay");
    }

    private void Die()
    {
        isInvulnerable = true;
        life = Mathf.Clamp(life - 1, 0, life);
        FXManager.Instance.PlayEffect("Explosion", t);
        FXManager.Instance.PlayEffect("ExplosionDecal", t);

        // destroy trail
        StopCoroutine("AdvanceMovement");
        StopCoroutine("AdvanceDecay");

        while (trail.Count > 0)
        {
            var trailObject = trail.Peek().gameObject;
            GameObject.Destroy(trailObject);
            trail.Dequeue();
        }

        deadlyTrail.Clear();
        
        if (life == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Respawn(true);
            StartCoroutine(TempInvulnerable());
        }
    }

    private IEnumerator AdvanceMovement()
    {
        while (true)
        {
            var from = t.position;
            var to = from + direction;
            t.rotation = GetRotationFromDirection(direction);

            if (!isInvulnerable)
            {
                var newObject = Instantiate(trailObject, from, t.rotation) as GameObject;
                deadlyTrail.Enqueue(newObject);
                trail.Enqueue(newObject);
            }

            while (from != to)
            {
                from = Vector3.MoveTowards(from, to, speed * Time.deltaTime);
                t.position = from;
                yield return null;
            }

            t.position = from;
        }
    }

    private float fallDelay = 0.3f;

    private IEnumerator AdvanceDecay()
    {
        while (isInvulnerable)
        {
            yield return null;
        }

        yield return new WaitForSeconds(ragaz);

        while (deadlyTrail.Count > 0)
        {
            yield return new WaitForSeconds(trailLife);
            deadlyTrail.Peek().GetComponent<FallAndFade>().Fall();
            deadlyTrail.Dequeue();
        }
        
        Die();
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyUp) && direction != Vector3.back)
        {
            MoveUp();
        }
        
        if (Input.GetKeyDown(keyRight) && direction != Vector3.left)
        {
            MoveRight();
        }

        if (Input.GetKeyDown(keyDown) && direction != Vector3.forward)
        {
            MoveDown();
        }

        if (Input.GetKeyDown(keyLeft) && direction != Vector3.right)
        {
            MoveLeft();
        }


    }

    public bool isInvulnerable;
    private float invulnerabilityTime = 3f;
    private FXBase inv;
    private IEnumerator TempInvulnerable()
    {
        isInvulnerable = true;
        if (inv != null)
        {
            inv.Stop();
        }
        inv = FXManager.Instance.PlayEffect("TweenAlphaInvulnerability", t);
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // to not die in generated trail we do this:
        if (other.CompareTag("FreshTrail"))
        {
            other.tag = "Player";
            return;
        }

        Debug.Log(gameObject.name + " collided with " + other.tag);

        if (other.CompareTag("Player") && !isInvulnerable && !hasShiled)
        {
            Die();
            return;
        }

        if (other.CompareTag("Environment"))
        {
            Die();
            return;
        }

        if (other.CompareTag("PowerupTime"))
        {
            MovePowerupRandomly(other.transform.parent);
            StartCoroutine("DelayDecay");
        }
    }

    private void MovePowerupRandomly(Transform powerupTransform)
    {
        bool areaOk = false;
        int randX = 0;
        int randZ = 0;
        RaycastHit hit;

        while (!areaOk)
        {
            randX = UnityEngine.Random.Range(-15, 16);
            randZ = UnityEngine.Random.Range(-15, 16);
            var cast = Physics.Raycast(new Vector3(randX, 100, randZ), Vector3.down, out hit);
            if (cast)
            {
                if (!hit.transform.CompareTag("Player") &&
                    !hit.transform.CompareTag("PowerupTime"))
                {
                    areaOk = true;
                }
            }
            else
            {
                areaOk = true;
            }
        }

        powerupTransform.transform.position = new Vector3(randX, 0, randZ);
    }

    private IEnumerator DelayDecay()
    {
        trailLife *= 2f;
        yield return new WaitForSeconds(2f);
        trailLife /= 2f;
    }

    private Quaternion GetRotationFromDirection(Vector3 d)
    {
        if (d == Vector3.forward)
        {
            return Quaternion.Euler(0, 0, 0);
        }

        if (d == Vector3.back)
        {
            return Quaternion.Euler(0, 180, 0);
        }

        if (d == Vector3.left)
        {
            return Quaternion.Euler(0, -90, 0);
        }

        if (d == Vector3.right)
        {
            return Quaternion.Euler(0, 90, 0);
        }

        return Quaternion.identity;
    }

    public void MoveRight()
    {
        if (direction != Vector3.left)
        {
            direction = Vector3.right;
        }
    }

    public void MoveLeft()
    {
        if (direction != Vector3.right)
        {
            direction = Vector3.left;
        }
    }

    public void MoveUp()
    {
        if (direction != Vector3.back)
        {
            direction = Vector3.forward;
        }
    }

    public void MoveDown()
    {
        if (direction != Vector3.forward)
        {
            direction = Vector3.back;
        }
    }
}