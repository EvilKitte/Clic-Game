using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterMotion : MonoBehaviour
{
    public enum movingModes { JumpSideToSide, Fly, JumpInAllDirections };
    public movingModes mode = movingModes.JumpSideToSide;

    public bool isJump = false;
    public float speed = 10.0f;

    private Vector3 motionDirection = new Vector3(0, 0, 0);
    private float secondsBetweenSpeedUp = 2f;
    private float nextSpeedUpTime;
    private AudioSource collisionAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        nextSpeedUpTime = Time.time + secondsBetweenSpeedUp;
        collisionAudioSource = GetComponent<AudioSource>();

        switch (mode)
        {

            case movingModes.JumpSideToSide:
                motionDirection = new Vector3(Random.Range(1, 20), Random.Range(1, 20), 0);
                motionDirection = speed * motionDirection.normalized;
                break;

            case movingModes.Fly:
                GetComponent<Rigidbody>().useGravity = false;
                break;

            case movingModes.JumpInAllDirections:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position.x > 25) || (transform.position.x < -25) || (transform.position.y > 35)
            || (transform.position.z > 25) || (transform.position.z < -25))
        {
            Destroy(gameObject);
        }

        switch (mode)
        {

            case movingModes.JumpSideToSide:
                if (!isJump)
                {
                    motionDirection.x = motionDirection.x > 1000 ? motionDirection.x *= 0.9f : motionDirection.x *= 1.1f;
                    motionDirection.y = motionDirection.y > 1000 ? motionDirection.y *= 0.9f : motionDirection.y *= 1.2f;

                    GetComponent<Rigidbody>().AddForce(motionDirection);

                    isJump = true;
                    motionDirection.x *= -1f;
                }
                break;

            case movingModes.Fly:
                break;

            case movingModes.JumpInAllDirections:
                if (!isJump)
                {
                    motionDirection = new Vector3(Random.Range(-20, 20), Random.Range(1, 20), Random.Range(-20, 20));
                    motionDirection = speed * motionDirection.normalized;

                    GetComponent<Rigidbody>().AddForce(motionDirection);
                    isJump = true;

                    if (Time.time >= nextSpeedUpTime)
                    {
                        speed = speed > 500 ? speed *= 0.9f : speed *= 1.1f;
                    }

                    nextSpeedUpTime = Time.time + secondsBetweenSpeedUp;
                }
                break;
        }
    }

    void FixedUpdate()
    {
        if(mode == movingModes.Fly)
        {
            motionDirection = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
            motionDirection = speed * motionDirection.normalized;

            GetComponent<Rigidbody>().AddForce(motionDirection);

            if (Time.time >= nextSpeedUpTime)
            {
                speed = speed > 300 ? speed *= 0.9f : speed *= 1.1f;
            }

            nextSpeedUpTime = Time.time + secondsBetweenSpeedUp;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            isJump = false;
        }

        collisionAudioSource.Play();
    }
}
