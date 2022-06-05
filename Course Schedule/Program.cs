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
      // how to solve this problem ? here we have to detect the cycle in the graph
      // ex - numOCurses = 3 [0, 1, 3] and prerequisites = [[0, 1],[1, 2], [2, 0]]
      // after creating the adj list for each course = [{0, [1]}, {1, [2]}, {2, [0]}]
      // from the adj list we can see -
        // a. to complete 0 we need to complete 1
        // b. to complete 1 we need to complete 2
        // c. to complete 2 we need to complete 0
        // as we can see we can not complete 0 because to complete 2 we need to complete 0 as well. We are in deadlocak state.
      
      
      // Step 1 - Create the adj list using dictionary for each course as key and list of courses as prerequisites
      Dictionary<int, List<int>> adj = new Dictionary<int, List<int>>();
      
      for(int i = 0 ; i < numCourses; i++) {
        adj.Add(i, new List<int>());
      }
      
      foreach(var prereq in  prerequisites) {
        var course = prereq[0];
        var precourse = prereq[1];
        var existingPrereq = adj[course];
        existingPrereq.Add(precourse);
        adj[course] = existingPrereq;
      }
      
      // visited list to check we have cycle r not ?
      HashSet<int> visited = new HashSet<int>();
      
      // Step 2 - for each course will perform DFS, if we find cycle we will return false and stop
      for(int i = 0; i < numCourses; i++) {
        if(!DFS(adj, visited, i)) return false;
      }
      
      return true;
    }
           
    private bool DFS(Dictionary<int, List<int>> adj, HashSet<int> visited, int course){
      if(visited.Contains(course)) return false; // we have found cycle
      
      var prereqlist = adj[course];
      if(prereqlist == null || !prereqlist.Any()) return true; // this tell when there are no prereq courses need to be completed, for any course we can tell the course can be completed as there are no prereq courses to be finish.
      
      visited.Add(course);
      foreach(var prereqcourse in prereqlist) {
        if(!DFS(adj, visited, prereqcourse)) return false;
      }
      
      adj[course] = null;
      visited.Remove(course);
      return true;
    }
  }
}
