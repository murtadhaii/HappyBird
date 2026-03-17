using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    Vector3 _initialPosition;
    private bool _birdWasLaunched = false;
    private float _timeSittingAround;
    [SerializeField] private float _launchPower = 500;
    

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);
    

        if (_birdWasLaunched && GetComponent<Rigidbody2D>().linearVelocity.magnitude <= 0.1)
        {
            _timeSittingAround += Time.deltaTime;
        }

        if (transform.position.y > 20)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (transform.position.y < -20)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (transform.position.x > 20)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (transform.position.x < -20)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (_timeSittingAround > 3)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            GetComponent<LineRenderer>().enabled = true;
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            Vector2 directionToInitialPosition = _initialPosition - transform.position;
            GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower);
            GetComponent<Rigidbody2D>().gravityScale = 1;
            _birdWasLaunched = true;
            GetComponent<LineRenderer>().enabled = false;
        }

        if (Mouse.current.leftButton.isPressed)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mousePos.z = 0;
            transform.position = mousePos;
        }
    }
}