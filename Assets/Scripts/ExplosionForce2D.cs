// Developed by Ananda Gupta
// info@onemaninteractive.com
// http://onemaninteractive.com

using UnityEngine;
using System.Collections;

public class ExplosionForce2D : MonoBehaviour
{
	public float Power;
	public float Radius;
	
		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void FixedUpdate ()
		{

			AddExplosionForce(GetComponent<Rigidbody2D>(), Power, new Vector3(0,0,0), Radius);
	
		}

		public static void AddExplosionForce (Rigidbody2D body, float expForce, Vector3 expPosition, float expRadius)
		{
				var dir = (body.transform.position - expPosition);
				float calc = 1 - (dir.magnitude / expRadius);
				if (calc <= 0) {
						calc = 0;		
				}

				body.AddForce (dir.normalized * expForce * calc);
		}


}
