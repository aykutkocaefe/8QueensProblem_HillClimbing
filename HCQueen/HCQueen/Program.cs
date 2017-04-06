using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCQueen
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int[] queenspos = new int[8] {3,2,1,4,3,2,1,2};
            int[,] chesstable = new int[8, 8];
            int[] bestopt = new int[3] {100,0,0};
            bool localMaxFlag = new bool();

            int[,] statistic = new int[35, 3];
            
            Stopwatch watch = new Stopwatch();
           

            for (int i = 0; i < 35; i++)
            {
                watch.Start();
                statistic[i, 1] = -1;
                statistic[i, 0] = 0;
                do
                {

                    restartpos(queenspos, rnd);
                    
                    localMaxFlag = true;

                    bestopt = new int[3] { 100, 0, 0 };
                    do
                    {
                        visitSquares(queenspos, chesstable);
                        localMaxFlag = lowestH(chesstable, bestopt, localMaxFlag);
                        substitution(queenspos, bestopt);
                        statistic[i, 0]++;
                        
                        
                    } while (localMaxFlag == true);
                    
                    statistic[i,1]++;
                    statistic[i, 0]--;
                    
                    
                } while (bestopt[0] != 0);
                Console.WriteLine("\n");
                for (int k = 7; k >= 0; k--)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Console.Write(chesstable[j, k] + " ");
                    }
                    Console.WriteLine("\n");
                }
                watch.Stop();
                statistic[i, 2] = (int)watch.ElapsedMilliseconds;
                watch.Reset();
            }

            for (int i = 0; i < 35; i++)
            {
                Console.Write(i+1 + ": ");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(statistic[i,j] +" ");
                }
                Console.WriteLine("ms");
            }



            Console.Read();

        }

        static void restartpos(int[] queenspos,Random rnd)
        {

            for (int i = 0; i < queenspos.Length; i++)
            {
                queenspos[i] = rnd.Next(0, 8);
            }

        }
        static int calculateH(int[] queenspos)
        {
            int h=0;
            for (int i = 1; i < 8; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (queenspos[i] == queenspos[j] || i - j == Math.Abs(queenspos[i] - queenspos[j]))
                    {
                        h++;
                    }
                }
            }
            return h;
        }
        static void visitSquares(int[] queenspos, int[,] chesstable)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    queenspos[i] = ((queenspos[i] + 1) % 8);
                    chesstable[i,queenspos[i]] = calculateH(queenspos);
                }
            }
        }
        static bool lowestH(int[,] chesstable,int[] bestopt,bool localMaxFlag)
        {
            localMaxFlag = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if(chesstable[i,j]<bestopt[0])
                    {
                        bestopt[0] = chesstable[i, j];
                        bestopt[1] = i;
                        bestopt[2] = j;
                        localMaxFlag = true;
                    }
                }
            }
            return localMaxFlag;
        }
        static void substitution(int[] queenspos,int[] bestopt)
        {
            queenspos[bestopt[1]] = bestopt[2];
        }
        
    }
}