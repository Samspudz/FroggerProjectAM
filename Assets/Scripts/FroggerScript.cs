using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FroggerScript : MonoBehaviour
{
    [SerializeField] private bool canMove;
    public bool isDead;
    [SerializeField] private bool onPlatform;
    [SerializeField] private bool onRiver;
    [SerializeField] private bool atBarrier;
    [SerializeField] Animator _anim;
    [SerializeField] GameManager gameManager;
    [SerializeField] AudioManager audioManager;
    [SerializeField] AudioSource _audi;

    void Start()
    {
        canMove = true;
        isDead = false;
        _anim = GetComponentInChildren<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        _audi = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
    }

    void Update()
    {
        PlayerUpdate();
    }

    void PlayerUpdate()
    {
        if (!isDead)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && canMove)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                PlayerMove(Vector3.up);
                StartCoroutine(MoveTime());
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && canMove)
            {
                transform.rotation = Quaternion.Euler(0, 0, 180);
                PlayerMove(Vector3.down);
                StartCoroutine(MoveTime());
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && canMove)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
                PlayerMove(Vector3.left);
                StartCoroutine(MoveTime());
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && canMove)
            {
                transform.rotation = Quaternion.Euler(0, 0, 270);
                PlayerMove(Vector3.right);
                StartCoroutine(MoveTime());
            }
        }
    }

    void PlayerMove(Vector3 direction)
    {
        Vector3 destination = transform.position + direction;

        Collider2D _barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0, LayerMask.GetMask("Barrier"));
        Collider2D _platform = Physics2D.OverlapBox(destination, Vector2.zero, 0, LayerMask.GetMask("Platform"));

        if (_barrier != null)
        {
            atBarrier = true;
            return;
        }
        else
        {
            atBarrier = false;
        }

        if (_platform != null)
        {
            onPlatform = true;
            transform.SetParent(_platform.transform);
        }
        else
        {
            onPlatform = false;
            transform.SetParent(null);
        }

        StartCoroutine(SmoothMove(destination));
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "KillVolume" && !isDead)
        {
            StartCoroutine(PlayerDeath(3));
        }

        if (other.gameObject.tag == "River" && !isDead)
        {
            onRiver = true;
        }

        if (other.gameObject.tag == "Scoring")
        {
            ScoringScript _score = other.gameObject.GetComponent<ScoringScript>();
            if (!_score.zoneTrigger)
            {
                gameManager.gameScore += _score.zoneScore;
                _score.zoneTrigger = true;
            }
        }

        if (other.gameObject.tag == "LilyPad")
        {
            LilyPadScript _pad = other.gameObject.GetComponent<LilyPadScript>();
            if (!_pad._occupied)
            {
                _pad._occupied = true;
                _pad._spr.enabled = true;
                gameManager.padCount--;
                gameManager.gameScore += 1000;
                gameManager.NewFrog();
                StartCoroutine(MoveTime());
                Destroy(gameObject);
            }
            else StartCoroutine(PlayerDeath(3));
        }
    }

    private void OnTriggerExit2D(Collider2D other) //ask 
    {
        if (other.gameObject.tag == "River") onRiver = false;
    }

    IEnumerator SmoothMove(Vector3 destination)
    {
        Vector3 startPosition = transform.position;
        _anim.SetTrigger("Hop");

        float elapsed = 0;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPosition, destination, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = destination;
    }

    IEnumerator MoveTime()
    {
        canMove = false;
        if (!atBarrier) _audi.PlayOneShot(audioManager.soundFX[1]);
        yield return new WaitForSeconds(0.15f);
        canMove = true;
        if (onRiver)
        {
            if (!onPlatform)
            {
                StartCoroutine(PlayerDeath(2));
            }    
        }
    }

    public void TimeOut()
    {
        StartCoroutine(PlayerDeath(3));
    }

    IEnumerator PlayerDeath(int sfx)
    {
        isDead = true;
        transform.SetParent(null);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        _anim.SetTrigger("Death");
        _audi.PlayOneShot(audioManager.soundFX[sfx]);
        yield return new WaitForSeconds(1.5f);
        isDead = false;
        gameManager.playerLives--;
        Destroy(gameObject);
        gameManager.NewFrog();
    }
}
