
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private Transform Xfor_camera;
    private Transform Xfor_parent;

    protected Vector3 localRotation;
    protected float cameraDistance = 10f;

    public float mouseSensitivity = 4f; //for panning the camera around
    public float scrollSensitivity = 4f; //for zoom in and out
    public float orbitDampening = 10f; //How long it takes for 
    public float scrollDampening = 6f;

    public bool cameraDisable = false;

	// Use this for initialization
	void Start () {
        this.Xfor_camera = this.transform;
        this.Xfor_parent = this.transform.parent;
	}
	
	// Late Update is called after Update() on every game object in scene
	void LateUpdate () {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            cameraDisable = !cameraDisable;
        }

        if (!cameraDisable) {
            //Rotation of the camera based on the x and y axis of the camera pointer
            if ((Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0))
            {
                localRotation.x += Input.GetAxis("Mouse X") * mouseSensitivity;
                localRotation.y -= Input.GetAxis("Mouse Y") * mouseSensitivity;

                //don't let y come outside bounds
                if ((localRotation.y < 60f))
                {
                    //Dont go below horizon
                    localRotation.y = 60f;
                }

                if (localRotation.y > 90f)
                {
                    //dont go above perpendicular
                    localRotation.y = 90f;
                }


                //Control the scroll input
                if (Input.GetAxis("Mouse ScrollWheel") != 0f)
                {
                    float scrollAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;

                    //Control the speed of the camera zoom
                    scrollAmount *= (this.cameraDistance * 0.3f);

                    this.cameraDistance += scrollAmount * -1f;


                    this.cameraDistance = Mathf.Clamp(this.cameraDistance, 1.5f, 10f);
                }
            }

            //Actual camera rotations
            Quaternion QT = Quaternion.Euler(localRotation.y, localRotation.x, 0);
            this.Xfor_parent.rotation = Quaternion.Lerp(this.Xfor_parent.rotation, QT, Time.deltaTime * orbitDampening);

            if (this.Xfor_camera.localPosition.z != cameraDistance * -1f) {
                this.Xfor_camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this.Xfor_camera.localPosition.z, this.cameraDistance * -1f, Time.deltaTime * scrollDampening));
            }
        }
	}
}
