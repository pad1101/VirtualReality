using System.Collections;
using System.Collections.Generic;
using OpenCvSharp;
using UnityEngine;

public class FaceDetector : MonoBehaviour
{
    // Start is called before the first frame update
    WebCamTexture _webCamTexture;

    CascadeClassifier cascade;

    OpenCvSharp.Rect MyFace;

    public float faceY;

    public float faceX;

    private OpenCvSharp.Rect previousFaces;

    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        _webCamTexture = new WebCamTexture(devices[0].name, 1280, 720);
        _webCamTexture.Play();
        cascade =
            new CascadeClassifier(Application.dataPath +
                @"/haarcascade_frontalface_default.xml");


         
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.mainTexture = _webCamTexture;
        Mat frame = OpenCvSharp.Unity.TextureToMat(_webCamTexture);

       // Debug.Log(_webCamTexture.height);

        findNewFace (frame);

        // immer mit vorherigen frame vergleichen und wenn abstand zu groß ist dann return;

      //  Debug.Log(frame.height);


        display (frame);
    }

    void findNewFace(Mat frame)
    {
        OpenCvSharp.Rect[] faces =
            cascade
                .DetectMultiScale(frame, 1.1, 2, HaarDetectionType.ScaleImage);

        if (previousFaces != null && previousFaces.Height * 0.6f >= faces[0].Height) return; // TODO: nullpointer abfangen
       
        
      //  if(previousFaces.Height * 0.6f >= faces[0].Height || previousFaces.Height * 1.4f <= faces[0].Height) return;

      //      if (faces[0].Height < 300f) return;

//Debug.Log(faces[0].Height);

                float widthCam = _webCamTexture.width;
                float heightCam = _webCamTexture.height;

        if (faces.Length >= 1)
        {
           // Debug.Log(faces[0].Location.X / widthCam); // TODO: WErte genau festlegen links:0.55  mitte: 0.4 rechts: 0.16
            MyFace = faces[0];
            faceY = faces[0].Location.Y / heightCam;
            faceX = faces[0].Location.X / widthCam;
        }

        previousFaces = faces[0];
    }

    void display(Mat frame)
    {
        if (MyFace != null)
        {
            frame.Rectangle(MyFace, new Scalar(250, 0, 0), 2);
        }

        Texture newtexture = OpenCvSharp.Unity.MatToTexture(frame);
        GetComponent<Renderer>().material.mainTexture = newtexture;
    }
}
