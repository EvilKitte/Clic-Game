using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    public float cameraSpeed = 0.05f;
    public bool canMove = false;

    private enum motionDirections { Up, Down, Right, Left};
    private motionDirections motionDirection = motionDirections.Right;

    //Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            switch(motionDirection)
            {

                case motionDirections.Up:
                    transform.Translate(0, cameraSpeed, 0);
                    if (transform.position.y >= 13)
                    {
                        motionDirection = motionDirections.Right;
                    }
                    break;

                case motionDirections.Down:
                    transform.Translate(0, -cameraSpeed, 0);
                    if (transform.position.y <= 7)
                    {
                        motionDirection = motionDirections.Left;
                    }
                    break;

                case motionDirections.Right:
                    transform.Translate(cameraSpeed, 0, 0);
                    if (transform.position.x >= 10)
                    {
                        motionDirection = motionDirections.Down;
                    }
                    break;

                case motionDirections.Left:
                    transform.Translate(-cameraSpeed, 0, 0);
                    if (transform.position.x <= -10)
                    {
                        motionDirection = motionDirections.Up;
                    }
                    break;
            }
        }
    }
}
