using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Generator {
	
	public class LevelGenerator
	{
		private const int ROOM_SPAWN_ATTEMPTS = 100;
		
		private const int ROOM_SIZE_CAP = 7;
			
		private const int ROOM_MIN_SIZE = 2;
		
		private int width;
		
		private int height;
		
		private int roomCount;
		
		private System.Random rnd;
		
		private TileState[,] map;
		
		private SortedList<float, Room> rooms = new SortedList<float, Room>();
		
		private List<Path> paths = new List<Path>();
		
		private float maxR;
		
		private float gamma;
		
		public LevelGenerator (int width, int height, int roomCount) {
			this.width = width;
			this.height = height;
			this.roomCount = roomCount;
			this.rnd = new System.Random();
			DetermineConstants();
		}
		
		public LevelGenerator (int width, int height, int roomCount, int seed) {
			this.width = width;
			this.height = height;
			this.roomCount = roomCount;
			this.rnd = new System.Random(seed);
			DetermineConstants();
		}
		
		private void DetermineConstants() {
			
			float min = Math.Min(width, height) / 10.0f;
			float max = Math.Min(width, height) / 5.0f;
			float step = Math.Min (width, height) / 20.0f;
			
			int i = 8;
			do {
				maxR = (float) Math.PI * i;
				
				for (gamma = max; gamma > min; gamma -= step) {
					
					float distance = Vector2.Distance(position(0.0f), position(2 * Mathf.PI));
					if (distance > (ROOM_SIZE_CAP + 3) * 1.2) {
						break;
					}
				}
				
				i -= 2;
			} while (gamma <= min && i >= 2);
			
			Debug.Log ("Gamma " + gamma);
			Debug.Log ("MaxR " + maxR);
		}

		private void PrintMap () {
			for (int i = 0; i < width; i++) {
				
				string line = "";
				for (int j = 0; j < height; j++) {
					
					switch (map[i, j]) {
					case TileState.EMTPY:
						line += ' ';
						break;
					case TileState.PAD:
						line += '@';
						break;
					case TileState.PATH:
						line += '#';
						break;
					case TileState.ROOM:
						line += 'R';
						break;
					}
					
				}
				
				Console.WriteLine (line);
			
				
			}
		}
		
		public bool Generate() {
			
			map = new TileState[width,height];
			for (int i = 0; i < roomCount; i++) {
				
				Room room = null;
				for (int attemps = 0; attemps < ROOM_SPAWN_ATTEMPTS; attemps++) {
					
					int roomHeight = rnd.Next(ROOM_MIN_SIZE, ROOM_SIZE_CAP);
					int roomWidth = rnd.Next(ROOM_MIN_SIZE, ROOM_SIZE_CAP);
					
					float r = (float) rnd.NextDouble() * maxR;
					Vector2 pos = position(r);
					
					int x = (int) Math.Round (pos.x - roomWidth / 2);
					int y = (int) Math.Round(pos.y - roomHeight / 2);
					
					room = new Room(x, y, roomHeight, roomWidth);
					if (CanPlaceRoom(room)) {
						rooms.Add(r, room);
						PlaceRoom(room);
						break;
					} else {
						room = null;
					}
				}
				
				if (room == null) {
					// No more rooms!
					Debug.Log ("Bailed adding rooms");
					break;
				}
				
			}
			
			ConnectRooms();
			
			PrintMap ();
			
			return true;
		}
		
		private Vector2 position(float r) {
			float mult = 0.5f * Mathf.Exp(-r / gamma);
			Vector2 pos = mult * new Vector2(width * Mathf.Cos (r), height * Mathf.Sin (r));
			pos.x = Mathf.Floor(pos.x) + width / 2;
			pos.y = Mathf.Floor(pos.y) + height / 2;
			
			return pos;
		}
			
		private Room ConnectRooms() {
			
			Console.WriteLine("Connecting rooms");
			
			RemovePadLayer();
			for (int i = rooms.Count - 1; i > 0; i--) {
				Connect (rooms[rooms.Keys[i]], rooms[rooms.Keys[i - 1]]);
			}
			
			return rooms.Last().Value;
		}
		
		
		private void RemovePadLayer() {
			
			foreach (Room room in rooms.Values) {
				RemovePadFromRoom(room);
			}
		}
		
		private void RemovePadFromRoom(Room room) {
			
			if (room.Pad == 0) {
				return;
			}
			
			int maxX = room.PaddedX + room.PaddedWidth;
			int maxY = room.PaddedY + room.PaddedHeight;
			
			for (int i = room.PaddedX; i <= maxX; i++) {
				if (map[i, room.PaddedY] == TileState.PAD) {
					map[i, room.PaddedY] = TileState.EMTPY;
				}
				if (map[i, maxY] == TileState.PAD) {
					map[i, maxY] = TileState.EMTPY;
				}
			}
			
			for (int j = room.PaddedY; j <= maxY; j++) {
				if (map[room.PaddedX, j] == TileState.PAD) {
					map[room.PaddedX, j] = TileState.EMTPY;
				}
				if (map[maxX, j] == TileState.PAD) {
					map[maxX, j] = TileState.EMTPY;
				}
			}
			
			room.Pad -= 1;
		}
		
		private void Connect(Room r1, Room r2) {
			
			RemovePadFromRoom(r1);
			RemovePadFromRoom(r2);
			RemovePadFromRoom(r2);
			
			Vector2 start;
			Vector2 end;
			
			double angle = Math.Atan2(r2.CenterY - r1.CenterY, r2.CenterX - r1.CenterX);
			if (-Math.PI / 4 < angle && angle <= Math.PI / 4) {
				start = new Vector2(r1.X + r1.Width + 1, r1.CenterY);
				end = new Vector2(r2.X - 1, r2.CenterY);
			} else if (Math.PI / 4 < angle && angle <= 3 * Math.PI / 4) {
				start = new Vector2(r1.CenterX, r1.Y + r1.Height + 1);
				end = new Vector2(r2.CenterX, r2.Y - 1);
			} else if (3 * Math.PI / 4 < angle || angle < - 3 * Math.PI / 4) {
				start = new Vector2(r1.X - 1, r1.CenterY);
				end = new Vector2(r2.X + r2.Width + 1, r2.CenterY);
			} else {
				start = new Vector2(r1.CenterX, r1.Y - 1);
				end = new Vector2(r2.CenterX, r2.Y + r2.Height + 1);
			}
			
			map[(int) start.x, (int) start.y] = TileState.EMTPY;
			List<Vector2> path = new AStar().findPath(map, start, end, r2);
			foreach (Vector2 p in path) {
				map[(int) p.x, (int) p.y] = TileState.PATH;
			}
			
			paths.Add(new Path(r1, r2, path));
			AddPad(r2);
			AddPad(r2);
			AddPad(r1);
		}
		
		private void AddPad(Room room) {
			
			room.Pad++;
			
			int maxX = room.PaddedX + room.PaddedWidth;
			int maxY = room.PaddedY + room.PaddedHeight;
			
			for (int i = room.PaddedX; i <= maxX; i++) {
				if (map[i, room.PaddedY] == TileState.EMTPY) {
					map[i, room.PaddedY] = TileState.PAD;
				}
				if (map[i, maxY] == TileState.EMTPY) {
					map[i, maxY] = TileState.PAD;
				}
			}
			
			for (int j = room.PaddedY; j <= maxY; j++) {
				if (map[room.PaddedX, j] == TileState.EMTPY) {
					map[room.PaddedX, j] = TileState.PAD;
				}
				if (map[maxX, j] == TileState.EMTPY) {
					map[maxX, j] = TileState.PAD;
				}
			}
			
			
		}
		
		private bool CanPlaceRoom(Room room) {
			
			int maxX = room.PaddedX + room.PaddedWidth;
			int maxY = room.PaddedY + room.PaddedHeight;
			if (room.PaddedX < 0 || room.PaddedY < 0 || maxX >= width || maxY >= height) {
				return false;
			}
			
			
			for (int i = room.PaddedX; i <= maxX; i++) {
				for (int j = room.PaddedY; j <= maxY; j++) {
					if (map[i, j] != TileState.EMTPY) {
						return false;
					}
				}
			}
			
			return true;
		}
		
		private void PlaceRoom(Room room) {
			
			int maxX = room.PaddedX + room.PaddedWidth;
			int maxY = room.PaddedY + room.PaddedHeight;
			
			for (int i = room.PaddedX; i <= maxX; i++) {
				for (int j = room.PaddedY; j <= maxY; j++) {
					
					map[i, j] = room.InRoom(i, j) ? TileState.ROOM : TileState.PAD;
				}
			}
			
		}
		
		public void Populate(Level level) {
			
			GameObject go = level.gameObject;
			
			Room firstRoom = rooms[rooms.Keys[0]];
			level.playerInstance.transform.position = new Vector3(firstRoom.CenterX * 10, 1, firstRoom.CenterY * 10);
			
			foreach (Room r in rooms.Values) {			
				
				GameObject container = new GameObject("Room (x, y) = (" + r.X + ", " + r.Y + ")");
				container.transform.parent = go.transform;
				container.transform.position = new Vector3(10 * r.X, 0, 10 * r.Y);
				
				for (int i = 0; i <= r.Width; i++) {
					for (int j = 0; j <= r.Height; j++) {
						GameObject floor = GameObject.Instantiate(level.floor) as GameObject;
						floor.transform.parent = container.transform;
						floor.transform.localPosition = new Vector3(10 * i, 0, 10 * j);
						
						GameObject roof = GameObject.Instantiate(level.roof) as GameObject;
						roof.transform.parent = container.transform;
						roof.transform.localPosition = new Vector3(10 * i, 10, 10 * j);
					}
				}
				
				for (int i = 0; i <= r.Width; i++) {
					if (!IsDoor(container, new Vector2(i, 0), Vector2.up)) {
						PlaceWall(container, level.wall, new Vector2(i, 0), Vector2.up);
					}
					if (!IsDoor(container, new Vector2(i, r.Height), -Vector2.up)) {
						PlaceWall(container, level.wall, new Vector2(i, r.Height), -Vector2.up);
					}
				}
				
				for (int j = 0; j <= r.Height; j++) {
					if (!IsDoor(container, new Vector2(0, j), Vector2.right)) {
						PlaceWall(container, level.wall, new Vector2(0, j), Vector2.right);
					}
					if (!IsDoor(container, new Vector2(r.Width, j), -Vector2.right)) {
						PlaceWall(container, level.wall, new Vector2(r.Width, j), -Vector2.right);
					}
				}
			}
			
			Vector2[] displacements = new Vector2[] {
				Vector2.right,
				Vector2.up,
				-Vector2.up,
				-Vector2.right
			};
			
			foreach (Path p in paths) {
				
				GameObject container = new GameObject(
					"Path from (" + p.Origin.X + ", " + p.Origin.Y + ") to (" + p.Destination.X + ", " + p.Destination.Y + ")"
				);
				container.transform.parent = go.transform;
				container.transform.position = Vector3.zero;
				
				
				List<Vector2> points = p.Points;
				
				foreach (Vector2 v in points) {
					GameObject floor = GameObject.Instantiate(level.floor) as GameObject;
					floor.transform.parent = container.transform;
					floor.transform.position = new Vector3(10 * v.x, 0, 10 * v.y);
					
					GameObject roof = GameObject.Instantiate(level.roof) as GameObject;
					roof.transform.parent = container.transform;
					roof.transform.localPosition = new Vector3(10 * v.x, 10, 10 * v.y);
				}
				
				for (int i = 1; i < p.Points.Count - 1; i++) {
					Vector2 from = points[i] - points[i - 1];
					Vector2 to = points[i] - points[i + 1];

					foreach (Vector2 w in displacements) {
						if (w == from || w == to) {
							continue;
						}
						
						PlaceWall(container, level.wall, points[i], w);
					}
					
				}
				
				Vector2 last = points[points.Count - 1];
				foreach (Vector2 w in displacements) {
					
					if (map[(int) (last.x + w.x), (int) (last.y + w.y)] == TileState.ROOM) {
						
						foreach (Vector2 v in displacements) {
							if (v == w || v == -w) {
								continue;
							}
							
							PlaceWall(container, level.wall, last, v);
						}
					}
				}
				
				
				Vector2 first = points[0];
				Vector2 firstTo = first - points[1];
				Debug.Log ("Point at " + first);
				Debug.Log (firstTo);
				foreach (Vector2 w in displacements) {
					
					if (map[(int) (first.x + w.x), (int) (first.y + w.y)] == TileState.ROOM) {
						
						foreach (Vector2 v in displacements) {
							if (v == firstTo || v == -w) {
								continue;
							}
							
							PlaceWall(container, level.wall, first, v);
						}
					}
				}
				
				
			}
			
		}
		
		private bool IsDoor(GameObject container, Vector2 pos, Vector2 facing) {
			Vector3 parent = container.transform.position;
			return map[(int) (parent.x / 10 + pos.x - facing.x), (int) (parent.z / 10 + pos.y - facing.y)] == TileState.PATH;
		}
		
		private void PlaceWall(GameObject container, GameObject wallPrefab, Vector2 pos, Vector2 facing) {
			
			GameObject wall = GameObject.Instantiate(wallPrefab) as GameObject;
			Vector2 final = pos * 10 - 5 * facing;
			wall.transform.parent = container.transform;
			wall.transform.localPosition = new Vector3(final.x, 5, final.y);
			wall.transform.localScale = new Vector3(1, 1, 1);
			float angle = Mathf.Acos(Vector2.Dot(Vector2.up, facing));
			if (facing.x < 0) {
				angle = -angle;
			}
				
			wall.transform.localRotation = Quaternion.Euler(90, angle * 180 / Mathf.PI, 0);
		}
		
		public enum TileState {
			EMTPY,
			PATH,
			ROOM,
			PAD
		};
	}

}
