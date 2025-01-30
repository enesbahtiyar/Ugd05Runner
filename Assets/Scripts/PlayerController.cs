using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;

    [Header("Settings")]
    [Tooltip("Bu Değişken oyuncunun hızını belirler")]
    [SerializeField] float speed;
    [Tooltip("Bu Değişken oyuncunun sağa sola kaç metre gideceğiniz ayarlar")]
    [SerializeField] float shift = 2;

    [HideInInspector] public Positions positions = Positions.onMiddle;

    [HideInInspector]public bool isLeft, isRight, isMiddle;

    bool isDead;
    [SerializeField] int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isMiddle = true;
    }


    void Update()
    {
        MoveCharacter();


    }


    /// <summary>
    /// Bu metod karakterin temel hareket kodu
    /// </summary>
    void MoveCharacter()
    {
        if(isDead) return;

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        #region Karakter Sınırlama Yöntemleri

        if (Input.GetKeyDown(KeyCode.A) && transform.position.x > -0.5f)
        {
            //transform.Translate(new Vector3(-shift, 0, 0));
            transform.DOMoveX(transform.position.x - shift, 0.5f).SetEase(Ease.Linear);
        }
        else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 0.5f)
        {
            //transform.Translate(new Vector3(shift, 0, 0));
            transform.DOMoveX(transform.position.x + shift, 0.5f).SetEase(Ease.Linear);
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

    /// <summary>
    /// ilk çarpıştığımız an
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            anim.SetBool("Death", true);
            isDead = true;
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
        if(other.CompareTag("Coin"))
        {
            score += 10;
            Destroy(other.gameObject);
        }
    }
}
