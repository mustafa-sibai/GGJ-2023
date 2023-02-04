using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraManager : MonoBehaviour
{
    public Camera theCamera; //Public reference to the main camera

    //Camera zoom / adjust
    public float boundsBuffer;
    public Tilemap tileMap; //Pick a tile map you want the camera's pos to be based on.

    //Camera scroll zoom
    //public int mouseScrollSpeed = 1;
   // public int defaultCameraZoom = 6;

    //Camera drag
  //  public float cameraDragSensitivity;
    private Vector3 initialCameraPosition;

    public float SPEED;

    private void Start()
    {
        UpdateCameraLocation();
        initialCameraPosition = theCamera.transform.position;
    }

    private void Update()
    {
      //  theCamera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * mouseScrollSpeed; // Makes the camera zoooom

        /*if (Input.GetMouseButton(2))
        {
            x = Input.GetAxis("Mouse X");
            y = Input.GetAxis("Mouse Y");
            theCamera.transform.Translate(new Vector3(-x, -y, 0) * cameraDragSensitivity);

            //theCamera.transform.position += new Vector3(-x, -y, 0) * cameraDragSensitivity;
            //theCamera.transform.position = Vector3.Lerp(theCamera.transform.position, new Vector3(-x, -y, 0), Time.deltaTime);

            //newPos = new Vector3(-x, -y, 0);
            //changedPos = newPos * cameraDragSensitivity;

        }*/

        if (Input.GetKeyDown(KeyCode.R))
        {
            theCamera.transform.position = initialCameraPosition;
            Debug.Log("somethign");
        }
        // theCamera.transform.position = Vector3.Lerp(theCamera.transform.position, changedPos, Time.deltaTime);

    }

    public void UpdateCameraLocation()
    {
        var (center, size) = CalculateCameraOrthoSize(); //Constantly sets the map size = to the function
        theCamera.transform.position = center;
        theCamera.orthographicSize = size;
    }

    private (Vector3 mapCenter, float size) CalculateCameraOrthoSize()
    {
        tileMap.CompressBounds();
        var bounds = tileMap.localBounds;

        bounds.Expand(boundsBuffer);

        var vertical = bounds.size.y;
        var horizontal = bounds.size.x * theCamera.pixelHeight / theCamera.pixelWidth;

        var size = Mathf.Max(horizontal, vertical) * 0.5f;
        var center = bounds.center + new Vector3(0, 0, -10);

        return (center, size);
    }
}
