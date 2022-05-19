using System;
using System.Collections.Generic;
using System.Linq;

namespace Course_Schedule
{
  class Program
  {
    static void Main(string[] args)
    {
      //var numCourses = 5;
      //var prerequisites = new int[5][] { new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 1, 3 }, new int[] { 1, 4 }, new int[] { 3, 4 } };
      var numCourses = 5;
      var prerequisites = new int[6][] { new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 1, 3 }, new int[] { 1, 4 }, new int[] { 3, 4 }, new int[] { 4, 3 } };
      Program p = new Program();
      var result = p.CanFinish(numCourses, prerequisites);
      Console.WriteLine(result);
    }

    public bool CanFinish(int numCourses, int[][] prerequisites)
    {
      // create the adjacency list
      Dictionary<int, List<int>> adj = new Dictionary<int, List<int>>();
      for(int i = 0; i < numCourses; i++)
      {
        adj.Add(i, new List<int>());
      }

      foreach(var pre in prerequisites)
      {
        int course = pre[0];
        int prereq = pre[1];
        var existingPrereq = adj[course];
        existingPrereq.Add(prereq);
        adj[course] = existingPrereq;
      }

      HashSet<int> visited = new HashSet<int>();
      for(int i = 0; i < numCourses; i++)
      {
        if (!DFS(adj, visited, i)) return false;
      }

      return true;
    }

    private bool DFS(Dictionary<int, List<int>> adj, HashSet<int> visited, int course)
    {
      if (visited.Contains(course)) return false;

      if (adj[course] == null || !adj[course].Any()) return true;

      visited.Add(course);
      var preCourses = adj[course];
      foreach (var preCourse in preCourses)
      {
        if(!DFS(adj, visited, preCourse)) return false;
      }

      visited.Remove(course);
      adj[course] = null;
      return true;
    }
  }
}
