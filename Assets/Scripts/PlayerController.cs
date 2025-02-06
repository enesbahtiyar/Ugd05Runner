using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Rigidbody rb;
    [SerializeField] public Animator anim;
    [SerializeField] AudioClip bonusSound, coinSound, deathSound, magnetCoinSound, shieldSound, winSound;
    [SerializeField] GameObject coinCollectedVFX, deathVFX, healthDeclineVFX, magnetVFX, wallBreakVFX, shieldVFX;
    [SerializeField] AudioSource playerSounds;

    [Header("Settings")]
    [Tooltip("Bu Değişken oyuncunun hızını belirler")]
    [SerializeField] float speed;
    [Tooltip("Bu Değişken oyuncunun sağa sola kaç metre gideceğiniz ayarlar")]
    [SerializeField] float shift = 2;

    [HideInInspector] public Positions positions = Positions.onMiddle;

    [HideInInspector] public bool isLeft, isRight, isMiddle;

    [HideInInspector] public bool isDead;
    [HideInInspector] public bool isStart;
    [SerializeField] public int score;
    [HideInInspector] public float floatScore;
    [HideInInspector] float passedTime;
    [SerializeField] int health;

    public bool is2xActive, isShieldActive, isMagnetActive;
    float beforeSpeed;

    bool isMove;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isMiddle = true;
    }


    void Update()
    {
        passedTime += Time.deltaTime;
        MoveCharacter();
    }


    /// <summary>
    /// Bu metod karakterin temel hareket kodu
    /// </summary>
    void MoveCharacter()
    {
        if (!isStart) return;
        if(isDead) return;

        if(is2xActive)
        {
            floatScore += Time.deltaTime;
        }

        floatScore += Time.deltaTime;

        if (floatScore > 1)
        {
            score += 1;
            floatScore = 0;
        }

        if(passedTime > 10)
        {
            speed += 0.3f;
            passedTime = 0;
        }


        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        #region Karakter Sınırlama Yöntemleri

        if (Input.GetKeyDown(KeyCode.A) && transform.position.x > -0.5f && !isMove)
        {
            //transform.Translate(new Vector3(-shift, 0, 0));
            transform.DOMoveX(transform.position.x - shift, 0.5f).SetEase(Ease.Linear).OnComplete(isMoveToFalse);

            isMove = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 0.5f && !isMove)
        {
            //transform.Translate(new Vector3(shift, 0, 0));
            transform.DOMoveX(transform.position.x + shift, 0.5f).SetEase(Ease.Linear).OnComplete(isMoveToFalse);

            isMove = true;
        }

        /*
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && positions != Positions.onLeft)
        {
            if(positions == Positions.onMiddle)
            {
                positions = Positions.onLeft;
            }
            else if(positions == Positions.onRight)
            {
                positions = Positions.onMiddle;
            }
            transform.Translate(new Vector3(-shift, 0, 0));
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && positions != Positions.onRight)
        {
            if (positions == Positions.onMiddle)
            {
                positions = Positions.onRight;
            }
            else if (positions == Positions.onLeft)
            {
                positions = Positions.onMiddle;
            }
            transform.Translate(new Vector3(shift, 0, 0));
        }
        */


        /*
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && isLeft == false)
        {
            if(isMiddle)
            {
                isLeft = true;
                isMiddle = false;
            }
            else if(isRight)
            {
                isMiddle = true;
                isRight = false;
            }
            transform.Translate(new Vector3(-shift, 0, 0));
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && isRight == false)
        {
            if (isMiddle)
            {
                isRight = true;
                isMiddle = false;
            }
            else if (isLeft)
            {
                isMiddle = true;
                isLeft = false;
            }
            transform.Translate(new Vector3(shift, 0, 0));
        }
        */

        /*
        if (Input.GetKeyDown(KeyCode.A) && transform.position.x > -0.5f)
        {
            transform.Translate(new Vector3(-shift, 0, 0));
        }
        else if(Input.GetKeyDown(KeyCode.D) && transform.position.x < 0.5f)
        {
            transform.Translate(new Vector3(shift, 0, 0));
        }
        */
        #endregion

    }


    void isMoveToFalse()
    {
        isMove = false;
    }

    /// <summary>
    /// ilk çarpıştığımız an
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            int damage = other.gameObject.GetComponent<Obstacle>().damage;

            if(isShieldActive)
            {
                Destroy(other.gameObject);
                isShieldActive = false;
                GameObject vfx = Instantiate(wallBreakVFX, other.transform.position, Quaternion.identity);
                Destroy(vfx,1f);
            }
            else
            {
                CheckHealth(damage, other.gameObject);
            }



        }
    }

    void CheckHealth(int damage, GameObject other)
    {
        health -= damage;

        if(health <= 0)
        {
            anim.SetBool("Death", true);
            playerSounds.PlayOneShot(deathSound);
            GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
            Destroy(vfx, 1f);
            isDead = true;
        }
        else
        {
            Destroy(other.gameObject);
            GameObject vfx = Instantiate(wallBreakVFX, other.transform.position, Quaternion.identity);
            Destroy(vfx, 1f);
            GameObject health = Instantiate(healthDeclineVFX, transform.position, Quaternion.identity, this.transform);
            Destroy(health, 2f);
        }

        if (score > 100)
        {
            playerSounds.PlayOneShot(winSound);
        }

    }

    /*
    /// <summary>
    /// çarpışmanın bittiği an
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision other)
    {
    }


    /// <summary>
    /// çarpışma boyunca çalışan metod
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay(Collision other)
    {
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Collectable"))
        {
            Collectables collectable = other.GetComponent<Collectables>();

            switch(collectable.collectablesEnum)
            {
                case CollectablesEnum.Coin:
                    AddScore(collectable.toBeAddedScore);
                    break;
                case CollectablesEnum.Shield:
                    ActivateShield();
                    break;
                case CollectablesEnum.Health:
                    AddHealth(collectable.toBeAddedHealth);
                    break;
                case CollectablesEnum.Score2X:
                    ActivateBonus();
                    break;
                case CollectablesEnum.SpeedUp:
                    AddSpeed(collectable.toBeAddedSpeed);
                    break;
                case CollectablesEnum.Magnet:
                    ActivateMagnet();
                    break;
            }

            Destroy(other.gameObject);
        }
    }

    private void AddSpeed(int toBeAddedSpeed)
    {
        beforeSpeed = speed;
        speed += toBeAddedSpeed;

        Invoke("BackToOriginalSpeed", 5f);
    }

    void BackToOriginalSpeed()
    {
        speed = beforeSpeed;
    }

    void AddScore(int toBeAddedScore)
    {
        if(isMagnetActive)
        {
            playerSounds.clip = magnetCoinSound;
            
            playerSounds.Play();
            

        }
        else
        {
            playerSounds.clip = coinSound;

            playerSounds.Play();
            
        }

        GameObject vfx = Instantiate(coinCollectedVFX, transform.position + Vector3.up, Quaternion.identity, this.transform);
        Destroy(vfx, 1f);
        if(is2xActive)
        {
            toBeAddedScore *= 2;
        }
        score += toBeAddedScore;
    }

    void ActivateShield()
    {
        isShieldActive = true;
        playerSounds.PlayOneShot(shieldSound);
        GameObject vfx = Instantiate(shieldVFX, transform.position, Quaternion.identity, this.transform);
        Destroy(vfx, 5f);
        Invoke("DeactivateShield", 5f);
    }
    
    void DeactivateShield()
    {
        isShieldActive = false;
    }

    void AddHealth(int toBeAddedHealth)
    {
        health += toBeAddedHealth;

        if (health <= 0)
        {
            anim.SetBool("Death", true);
            isDead = true;
        }
    }

    void ActivateBonus()
    {
        is2xActive = true;
        AudioSource.PlayClipAtPoint(bonusSound, transform.position);
        Invoke("DeactivateBonus", 5f);
    }

    void DeactivateBonus()
    {
        is2xActive = false;
    }

    void ActivateMagnet()
    {
        isMagnetActive = true;
        GameObject vfx = Instantiate(magnetVFX, this.transform.position + Vector3.up, Quaternion.identity, this.transform);
        Destroy(vfx, 5f);
        Invoke("DeactivateMagnet", 5f);
    }

    void DeactivateMagnet()
    {
        isMagnetActive = false;
    }
}
