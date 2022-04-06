using UnityEngine;

public class Move : MonoBehaviour
{
    public float Movespeed = 1.0f;
    public float Turnspeed = 90f;
    public bool ControlsOn = false;
    public float jumpForce = 1.5f;

    private void FixedUpdate()
    {
        if (ControlsOn == false)
            return;

        float vert = Input.GetAxis("Vertical");
        float horz = Input.GetAxis("Horizontal");
        this.transform.Translate(Vector3.forward * vert * Movespeed * Time.deltaTime);
        this.transform.localRotation *= Quaternion.AngleAxis(horz * Turnspeed * Time.deltaTime, Vector3.up);

        if (Input.GetKey(KeyCode.Space) == true)
        {
            this.transform.Translate(Vector3.up * jumpForce * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Z) == true)
        {
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            go.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 3f, this.transform.position.z);
            go.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
    }
}
