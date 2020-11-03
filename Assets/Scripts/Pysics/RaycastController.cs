using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RaycastController : MonoBehaviour
{

	public LayerMask collisionMask;

	public const float skinWidth = .015f;
	const float dstBetweenRays = .25f;
	[HideInInspector]
	public int horizontalRayCount;
	[HideInInspector]
	public int verticalRayCount;

	[HideInInspector]
	public float horizontalRaySpacing;
	[HideInInspector]
	public float verticalRaySpacing;

	[HideInInspector]
	public new BoxCollider2D collider;
	public RaycastOrigins raycastOrigins;

	private Vector2 colliderOriginalSize;
	private Vector2 colliderOriginalOffset;

	protected virtual void Awake()
	{
		collider = GetComponent<BoxCollider2D>();

		colliderOriginalSize = collider.size;
		colliderOriginalOffset = collider.offset;
	}

	protected virtual void Start()
	{
		CalculateRaySpacing();
	}

	public void ResetColliderSize()
    {
		collider.size = colliderOriginalSize;
		collider.offset = colliderOriginalOffset;

		CalculateRaySpacing();
    }

	public void ShrinkColliderSize()
    {
		collider.offset = new Vector2(collider.offset.x, colliderOriginalOffset.y - (colliderOriginalSize.y / 2) / 2);
		collider.size = new Vector2(collider.size.x, colliderOriginalSize.y / 2);

		CalculateRaySpacing();
    }

	public void UpdateRaycastOrigins()
	{
		Bounds bounds = collider.bounds;
		bounds.Expand(skinWidth * -2);

		raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
		raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
		raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
		raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
	}

	public void CalculateRaySpacing()
	{
		Bounds bounds = collider.bounds;
		bounds.Expand(skinWidth * -2);

		float boundsWidth = bounds.size.x;
		float boundsHeight = bounds.size.y;

		horizontalRayCount = Mathf.RoundToInt(boundsHeight / dstBetweenRays);
		verticalRayCount = Mathf.RoundToInt(boundsWidth / dstBetweenRays);

		horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
		verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
	}

	public struct RaycastOrigins
	{
		public Vector2 topLeft, topRight;
		public Vector2 bottomLeft, bottomRight;
	}
}