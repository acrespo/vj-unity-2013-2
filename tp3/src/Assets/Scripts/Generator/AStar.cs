using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Generator
{
	public class AStar
	{
		private delegate float Cost(Node n);
			
		public List<Vector2> findPath(LevelGenerator.TileState[,] map,  Vector2 start, Vector2 end, Room destination) {
			
			/*
			 * g(n) = costo hasta n
			 * h(n) = costo estimado desde n al goal
			 * f(n) = g(n) + h(n)
			 * 
			 * while true:
			 * 	 saco n del border set / menor f, tie break con g
			 *   pongo n en el camino
			 *   si n == goal
			 *     break
			 *   exploto n
			 *     pongo los hijos de n en el border set
			 *   
			 */
			
			int width = map.GetLength(0);
			int height = map.GetLength(1);
			
			SortedDictionary<Node, Node> border = new SortedDictionary<Node, Node>(new NodeComparer());
			HashSet<Vector2> visited = new HashSet<Vector2>();
			Cost h = (n) => (end - n.p).magnitude;
			
			Node first = new Node(null, start, h);
			Node last = null;
			
			border.Add(first, first);
			visited.Add(first.p);
			
			Vector2[] displacements = new Vector2[] {
				Vector2.right,
				Vector2.up,
				-Vector2.up,
				-Vector2.right
			};
		
			
			Console.WriteLine ("start={0}, end={1}", start, end);

			while (border.Count > 0) {
				Node node = border.First().Key;
				border.Remove(node);
				
				if (TouchesRoom(destination, node.p)) {
					last = node;
					break;
				}
				
				foreach (Vector2 delta in displacements) {
					Vector2 child = node.p + delta;
					if (child.x < 0 || child.x >= width || child.y < 0 || child.y >= height) {
						continue;
					}
					
					LevelGenerator.TileState tile = map[(int) child.x, (int) child.y];
					if (!visited.Contains(child) && tile == LevelGenerator.TileState.EMTPY) {
						visited.Add(child);
						Node childNode = new Node(node, child, h);
						border.Add(childNode, childNode);
					}
				}
				
			}
			
			List<Vector2> path = new List<Vector2>();
			while (last != null) {
				path.Add(last.p);
				last = last.parent;
			}
			Console.WriteLine(path.Count);
			
			return path;
		}
		
		private bool TouchesRoom(Room r, Vector2 p) {
			return r.X - 1 <= p.x && p.x <= r.X + 1 + r.Width && r.Y - 1 <= p.y && p.y <= r.Y + r.Height + 1;
		}
		
		private class Node {
			public Node parent;
			public Vector2 p;
			public float g = 0;
			public float h = 0;
			
			public Node (Node parent, Vector2 p, Cost h) {
				this.parent = parent;
				this.p = p;
				if (parent != null) {
					
					this.g = parent.g + 1;
					if (parent.parent != null) {
						Vector2 dir = p - parent.p;
						if (parent.parent.p + 2 * dir != p) {
							this.g++;
						}
					} else {
					}
				}
				this.h = h(this);
			}
				
			public float f {
				get {
					return g + h;
				}
			}
			
			public override bool Equals (object obj)
			{
				if (obj == null)
					return false;
				if (ReferenceEquals (this, obj))
					return true;
				if (obj.GetType () != typeof(Node))
					return false;
				Generator.AStar.Node other = (Generator.AStar.Node)obj;
				return p == other.p;
			}


			public override int GetHashCode ()
			{
				unchecked {
					return p.GetHashCode ();
				}
			}


		}
		
		private class NodeComparer : IComparer<Node> {
			
			public int Compare(Node n1, Node n2) {
				
				if (n1.Equals(n2)) {
					return 0;
				}
				
				return (n1.f < n2.f ? -1 : (n1.f == n2.f ? (n1.g <= n2.g ? -1 : 1) : 1));
			}
		}
	}
}

