using System.Collections;
using TarodevController;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    Vector2 CheckpointPos;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    [SerializeField] private ScriptableStats _stats;

    [SerializeField] private float StartingHealth;
    public float CurrentHealth { get; private set; }

    float multiplier, duration;
    bool timerActive = false;
    float currentTime;
    bool speed,jump;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        GetComponent<Rigidbody2D>();
        CheckpointPos = transform.position;
    }

  
    private void Awake()
    {
        CurrentHealth = StartingHealth;
    }

    public void TakeDamage(float _damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - _damage, 0,  StartingHealth);

        if (CurrentHealth > 0 )
        {
             
          
        }

        else
        {
            //player dies
            SceneManager.LoadScene("Game Over");

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PitFall"))
        {
 
            sr.enabled = false;
            StartCoroutine(Respawn(0.1f));
            TakeDamage(0.5f);
            sr.enabled = true;
        }

        if (collision.CompareTag("heal"))
        {
            sr.enabled = false;
            StartCoroutine(retrieveSpecificItems("http://localhost/Module2/FetchSpecific.php", "heal"));
            collision.gameObject.SetActive(false);
            sr.enabled = true;
        }

        if (collision.CompareTag("speed"))
        {
            sr.enabled = false;
            speed = true;
            StartCoroutine(retrieveSpecificItems("http://localhost/Module2/FetchSpecific.php", "speed"));
            collision.gameObject.SetActive(false);
            sr.enabled = true;
        }

        if (collision.CompareTag("jump"))
        {
            sr.enabled = false;
            jump = true;
            StartCoroutine(retrieveSpecificItems("http://localhost/Module2/FetchSpecific.php", "jump"));
            collision.gameObject.SetActive(false);
            sr.enabled = true;
        }

        if (collision.CompareTag("enemy"))
        {
            sr.enabled = false;
            if (!speed)
            {
                TakeDamage(1);
            }
            sr.enabled = true;
        }
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        CheckpointPos = pos;
    }

    IEnumerator Respawn(float duration)
    {
        yield return new WaitForSeconds(duration);
        transform.position = CheckpointPos;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
            StartCoroutine(Respawn(0.5f));
        }

        if (timerActive == true)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                timerActive=false;
                if (speed == true)
                {
                    _stats.MaxSpeed -= multiplier;
                    speed = false;

                }
                if (jump == true)
                {
                    _stats.JumpPower -= multiplier;
                    jump = false;

                }

            }
        }
    
    }

    IEnumerator retrieveSpecificItems(string url, string tag)
    {
        WWWForm form = new WWWForm();
        form.AddField("tag", tag);

        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            var text = uwr.downloadHandler.text;
            string[] power = text.Split(' ');


            timerActive = true;
            currentTime = float.Parse(power[1]);
            multiplier = float.Parse(power[0]);
            

            if (tag == "speed")
            {
                _stats.MaxSpeed += multiplier;
            }
            else if(tag == "jump")
            {
                _stats.JumpPower += multiplier;
            }
            else
            {
                CurrentHealth = Mathf.Clamp(CurrentHealth + multiplier, 0, StartingHealth);
            }
            

        }
    }

}


