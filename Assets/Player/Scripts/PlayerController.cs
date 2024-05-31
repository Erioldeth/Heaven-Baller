using UnityEngine;

public class PlayerController : EntityController
{
	[SerializeField] private float jumpForce;
	private bool isGrounded;

	protected override void LateUpdate()
	{
		base.LateUpdate();
		if(removed)
		{
			GameManager.isGameOver = true;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Ground"))
		{
			isGrounded = true;
		}
	}

	protected override void Move()
	{
		float hInput = Input.GetAxis("Horizontal");
		float vInput = Input.GetAxis("Vertical");
		Vector3 direction = new(hInput, 0, vInput);
		body.AddForce(moveForce * direction.normalized, ForceMode.Acceleration);

		if(isGrounded && Input.GetKeyDown(KeyCode.Space))
		{
			body.AddForce(jumpForce * Vector3.up, ForceMode.VelocityChange);
			isGrounded = false;
		}
	}
}