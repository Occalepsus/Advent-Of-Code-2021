using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    public struct Node
    {
        public string name;
        public List<Node> neighbours;

        public Node(string name)
        {
            this.name = name;
            neighbours = new List<Node>(); 
        }

        public bool IsSmall
        {
            get => 96 < name[0] && name[0] < 123;
        }

        public override string ToString()
        {
            return name;
        }
    }

    class Day12
    {
        private Node start;
        private Node end;

        private List<Node> nodes = new List<Node>();

        public Day12()
        {
            string[] inputs = Global.GetArgs("Day12.txt");

            ManageData(inputs);

            Console.WriteLine(Part2());
        }

        public string Part1()
        {
            List<Node[]> paths = new List<Node[]>();
            Stack<Node> path = new Stack<Node>();
            Stack<int> indexList = new Stack<int>();

            //On commence à start
            path.Push(start);
            indexList.Push(-1);

            //Tant que le chemin contient start
            while (path.Count > 0)
            {
                //On ajoute 1 à l'indice du dernier de la file
                indexList.Push(indexList.Pop() + 1);

                //Si le dernier node est end
                if (path.Peek().Equals(end))
                {
                    Node[] closedPath = path.ToArray();
                    Array.Reverse(closedPath);

                    //On ajoute le chemins aux chemins
                    paths.Add(closedPath);
                    Console.WriteLine(PathToString(closedPath) + "good");

                    //On dépile un coup
                    path.Pop();
                    indexList.Pop();
                }
                //Sinon si le dernier node n'a plus de voisin dispo
                else if (indexList.Peek() >= path.Peek().neighbours.Count)
                {
                    //On dépile un coup
                    path.Pop();
                    indexList.Pop();
                }
                else
                {
                    //On trouve le voisin suivant du dernier node de la file
                    Node next = path.Peek().neighbours[indexList.Peek()];

                    //Si ce voision n'est pas (petit et déjà dans dans la liste)
                    if (!(next.IsSmall && path.Contains(next)))
                    {
                        //On l'empile
                        path.Push(next);
                        indexList.Push(-1);
                    }
                }
                Node[] temp = path.ToArray();
                Array.Reverse(temp);
                //Console.WriteLine(PathToString(temp));
            }
            return paths.Count.ToString();           
        }
        
        
        public string Part2()
        {
            bool notVisitedTwice = true;

            List<Node[]> paths = new List<Node[]>();
            Stack<Node> path = new Stack<Node>();
            Stack<int> indexList = new Stack<int>();
            Stack<bool> visitedTwice = new Stack<bool>();

            //On commence à start
            path.Push(start);
            indexList.Push(-1);
            visitedTwice.Push(false);

            //Tant que le chemin contient start
            while (path.Count > 0)
            {
                //On ajoute 1 à l'indice du dernier de la file
                indexList.Push(indexList.Pop() + 1);

                //Si le dernier node est end
                if (path.Peek().Equals(end))
                {
                    Node[] closedPath = path.ToArray();
                    Array.Reverse(closedPath);

                    //On ajoute le chemins aux chemins
                    paths.Add(closedPath);
                    Console.WriteLine(PathToString(closedPath) + "good");

                    //On dépile un coup
                    path.Pop();
                    indexList.Pop();
                    visitedTwice.Pop();
                }
                //Sinon si le dernier node n'a plus de voisin dispo
                else if (indexList.Peek() >= path.Peek().neighbours.Count)
                {
                    //On dépile un coup
                    path.Pop();
                    indexList.Pop();
                    notVisitedTwice |= visitedTwice.Pop();
                }
                else
                {
                    //On trouve le voisin suivant du dernier node de la file
                    Node next = path.Peek().neighbours[indexList.Peek()];

                    //Si ce voision n'est pas (petit et déjà dans dans la liste)
                    if (!(next.IsSmall && path.Contains(next)))
                    {
                        //On l'empile
                        path.Push(next);
                        indexList.Push(-1);
                        visitedTwice.Push(false);
                    }
                    //Sinon si on a pas encore visité de deuxième cave
                    else if (next.IsSmall && notVisitedTwice && !next.Equals(start))
                    {
                        notVisitedTwice = false;
                        path.Push(next);
                        indexList.Push(-1);
                        visitedTwice.Push(true);
                    }
                }
                Node[] temp = path.ToArray();
                Array.Reverse(temp);
                //Console.WriteLine(PathToString(temp));
            }
            return paths.Count.ToString();           
        }

        private string PathToString(Node[] path)
        {
            string res = "";
            for (int i = 0; i < path.Length; i++)
            {
                res += path[i].name + " - ";
            }
            return res;
        }

        private void ManageData(string[] inputs)
        {
            string[][] path = new string[inputs.Length][];
            for (int i = 0; i < inputs.Length; i++)
            {
                path[i] = inputs[i].Split(new char[] { '-' });

                for (int idx = 0; idx < nodes.Count; idx++)
                {
                    if (path[i][0] == nodes[idx].name) goto EndA;
                }
                nodes.Add(new Node(path[i][0]));
            EndA:;

                for (int idx = 0; idx < nodes.Count; idx++)
                {
                    if (path[i][1] == nodes[idx].name) goto EndB;
                }
                nodes.Add(new Node(path[i][1]));
            EndB:;
            }

            start = nodes.Find(a => a.name == "start");
            end = nodes.Find(a => a.name == "end");

            for (int i = 0; i < path.Length; i++)
            {
                Node nodeA = nodes.Find(a => a.name == path[i][0]);
                Node nodeB = nodes.Find(b => b.name == path[i][1]);

                if (!nodeA.neighbours.Contains(nodeB)) nodeA.neighbours.Add(nodeB);
                if (!nodeB.neighbours.Contains(nodeA)) nodeB.neighbours.Add(nodeA);
            }
        }
    }
}
