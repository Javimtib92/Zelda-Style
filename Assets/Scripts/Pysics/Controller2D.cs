using UnityEngine;

namespace UnityCore.Controller
{
	public class Controller2D : RaycastController
	{

		// Members
		public CollisionInfo collisions;

		public struct CollisionInfo
		{
			public bool above, below;
			public bool left, right;

			public Vector2 moveAmountOld;

			public void Reset()
			{
				above = below = false;
				left = right = false;
			}
		}

		#region Unity Functions
		protected override void Start()
		{
			base.Start();

		}
		#endregion

		#region Public Functions
		public Vector2 Move(Vector2 moveAmount)
		{
			UpdateRaycastOrigins();

			collisions.Reset();
			collisions.moveAmountOld = moveAmount;

			HorizontalCollisions(ref moveAmount);
			VerticalCollisions(ref moveAmount);

			transform.Translate(moveAmount);

			return moveAmount;
		}
		#endregion
		protected virtual void HorizontalCollisions(ref Vector2 moveAmount)
		{
			float directionX = Mathf.Sign(moveAmount.x);
			float rayLength = Mathf.Abs(moveAmount.x) + skinWidth;

			if (Mathf.Abs(moveAmount.x) < skinWidth)
			{
				rayLength = 2 * skinWidth;
			}

			for (int i = 0; i < horizontalRayCount; i++)
			{
				Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
				rayOrigin += Vector2.up * (horizontalRaySpacing * i);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

				Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

				if (hit)
				{
					if (hit.distance == 0)
					{
						continue;
					}

					moveAmount.x = (hit.distance - skinWidth) * directionX;
					rayLength = hit.distance;

					collisions.left = directionX == -1;
					collisions.right = directionX == 1;
				}
			}

		}

		protected virtual void VerticalCollisions(ref Vector2 moveAmount)
		{
			float directionY = Mathf.Sign(moveAmount.y);
			float rayLength = Mathf.Abs(moveAmount.y) + skinWidth;

			for (int i = 0; i < verticalRayCount; i++)
			{

				Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
				rayOrigin += Vector2.right * (verticalRaySpacing * i + moveAmount.x);
				RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

				Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

				if (hit)
				{
					moveAmount.y = (hit.distance - skinWidth) * directionY;
					rayLength = hit.distance;

					collisions.below = directionY == -1;
					collisions.above = directionY == 1;
				}
			}
		}
	}
}