using System;
using UnityEngine;

namespace Generator {

	public class Room
	{
		private int x;
		
		private int y;
		
		private int height;
		
		private int width;
		
		private int pad = 3;
		
		public Room (int x, int y, int height, int width) {
			this.x = x;
			this.y = y;
			this.height = height;
			this.width = width;
		}
	
		public int X {
			get {
				return this.x;
			}
		}
	
		public int Y {
			get {
				return this.y;
			}
		}
	
		public int Height {
			get {
				return this.height;
			}
		}
	
		public int Width {
			get {
				return this.width;
			}
		}
		
		public int PaddedHeight {
			get {
				return this.height + 2 * pad;
			}
		}
		
		public int PaddedWidth {
			get {
				return this.width + 2 * pad;
			}
		}
		
		public int PaddedX {
			get {
				return this.x - pad;
			}
		}
		
		public int PaddedY {
			get {
				return this.y - pad;
			}
		}
		
		public int CenterX {
			get {
				return x + width / 2;
			}
		}
		
		public int CenterY {
			get {
				return y + height / 2;
			}
		}
		
		public bool InRoom(int x, int y) {
			return this.x <= x && x <= this.x + width && this.y <= y && y <= this.y + height;
		}
		
		public int Pad {
			get {
				return this.pad;
			}
			set {
				this.pad = value;
			}
		}
			
		public Vector2 Vector {
			get {
				return new Vector2(x, y);
			}
		}
		
		public Vector2 CenterVector {
			get {
				return new Vector2(CenterX, CenterY);
			}
		}
	}

}