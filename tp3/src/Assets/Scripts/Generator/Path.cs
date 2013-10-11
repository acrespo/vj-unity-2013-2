using System.Collections.Generic;
using UnityEngine;

namespace Generator
{
	public class Path
	{
		private List<Vector2> points;
		
		private Room origin;
		
		private Room destination;
		
		public Path (Room origin, Room destination, List<Vector2> points) {
			this.origin = origin;
			this.destination = destination;
			this.points = points;
		}
		
		public Room Origin {
			get {
				return this.origin;
			}
		}

		public Room Destination {
			get {
				return this.destination;
			}
		}
		
		public List<Vector2> Points {
			get {
				return this.points;
			}
		}
	}
}

