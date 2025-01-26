using UnityEngine;
using UnityEngine.Rendering;


public enum Positions
{
    onLeft,
    onRight,
    onMiddle
}


public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator anim;

    [SerializeField] float speed;
    [SerializeField] float shift = 2;

    [SerializeField] Positions positions = Positions.onMiddle;

    [SerializeField] bool isLeft, isRight, isMiddle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isMiddle = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        #region Karakter Sınırlama Yöntemleri

        if (Input.GetKeyDown(KeyCode.A) && transform.position.x > -0.5f)
        {
            transform.Translate(new Vector3(-shift, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 0.5f)
        {
            transform.Translate(new Vector3(shift, 0, 0));
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

    private void LateUpdate()
    {
    }
}
