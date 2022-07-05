using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Steuerung der Spielfigur.
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// Laufgeschwindigkeit der Spielfigur.
    /// </summary>
    // [HideInInspector]
    public float speed { get;  set;}

    /// <summary>
    /// Das grafische Modell, u.a. für die Drehung in Laufrichtung.
    /// </summary>
    public GameObject model;

    /// <summary>
    /// Der Winkel zu dem sich die Figur um die eigene Achse drehen soll.
    /// </summary>
    private float towardsY = -90f;

    /// <summary>
    /// Kraft, mit der nach oben gesprungen wird.
    /// </summary>
    private float jumpPush = 5f;

    /// <summary>
    /// Verstärkung der Gravitation, damit die Spielfigur schneller fällt.
    /// </summary>
    private float extraGravity = -20f;

    /// <summary>
    /// Zeiger auf die Physik-Komponente
    /// </summary>
    private Rigidbody rigid;

    /// <summary>
    /// Zeiger auf die Animations-Komponente der Spielfigur
    /// </summary>
    private Animator anim;

    /// <summary>
    /// Wenn false, fällt oder springt die Spielfigur
    /// </summary>
    private bool onGround = false;

    /// <summary>
    /// Webcam-Device, um die Spielfigur zu steuern
    /// </summary>
    FaceDetector faceDetector;

    /// <summary>
    /// Sound, der bei Springen entsteht
    /// </summary>
    public AudioSource jumpAudio;

    public void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        faceDetector = (FaceDetector) FindObjectOfType(typeof (FaceDetector));
        speed = 0.5f;
    }

    // Update is called once per frame
    public void Update()
    {

        anim.SetFloat("forward", 0.7f);

    //Debug.Log(faceDetector.faceX);


//relativ möglich
        if (faceDetector.faceX <= 0.28)
        {
            towardsY = -45f;
            transform.position += speed * Vector3.forward; // nochmal anschauen
            transform.position += speed * transform.forward;
        }
        else if (
            faceDetector.faceX >= 0.55 //nach links gehen
        )
        {
            towardsY = -135f;
            transform.position += -1 * speed * Vector3.forward;
            transform.position += speed * transform.forward;
        }
        else
        {
            towardsY = -90f;
            transform.position += speed * transform.forward;
        }

        model.transform.rotation =
            Quaternion
                .Lerp(model.transform.rotation,
                Quaternion.Euler(0f, towardsY, 0f),
                Time.deltaTime * 10f);


        // springen
        Debug.Log(faceDetector.faceX);

        RaycastHit hitInfo;
        onGround =
            Physics
                .Raycast(transform.position + (Vector3.up * 0.1f),
                Vector3.down,
                out hitInfo,
                0.5f);
        anim.SetBool("grounded", onGround);

         if (faceDetector.faceY > 0f && faceDetector.faceY < 0.15f && onGround)
        {
            Vector3 power = rigid.velocity;
            power.y = jumpPush;
            rigid.velocity = power;
            jumpAudio.Play();
            rigid.AddForce(new Vector3(0f, extraGravity, 0f));
        }

        
    }
    
}
