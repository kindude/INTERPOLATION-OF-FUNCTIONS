using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace АИМВ_1
{
    class DifferenceTable
    {
        private double h;
        private double start;
        private double stop;
        private double[,] diftable;
        private string[,] diftableChar;
        private int n, m;
        private double[,] NewtonFirst,NewtonSecond;
        public DifferenceTable()
        {

            Input();

        }

        public void Input()
        {
            Console.Write("Введите начало интервала:");
            start = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите конец интервала:");
            stop = Convert.ToDouble(Console.ReadLine());
        }

        public void Step()
        {
            h = (stop - start) / 10;
        }

        public void InitMatr()
        {
            n = 11;
            m = 13;
            diftable = new double[n, m];
           for(int i=0;i<n;i++)
            {
                for(int j=0;j<m;j++)
                {
                    diftable[i, j] = -3333;
                }
            }
        }
        public void FormDiffTable()
        {
            diftable[0, 0] = start;
            diftable[0, 1] = Math.Tan(Math.Pow(start, 3));
            for (int i = 1;i<n;i++)
            {

                diftable[i, 0] = diftable[i - 1, 0] + h;
                diftable[i,1] = Math.Tan(Math.Pow(diftable[i,0], 3));
                    
                
            }
        }

        
        public void DisplayMatr()
        {
            diftableChar = new string[n+1, m];
            for(int i=0;i<n;i++)
            {
                for(int j=0;j<m;j++)
                {
                    if (diftable[i, j] == -3333)
                    {
                        int p = i + 1;
                        diftableChar[p, j] = " -----   ";
                    }
                    else
                    {
                        int p = i + 1;
                        string tmp = string.Format("{0:f6}  ", diftable[i, j]);
                        diftableChar[p, j] = tmp;
                    }
                }
               


            }

            diftableChar[0, 0] = "X          " ;
            diftableChar[0, 1] = "Y         ";
            for (int i = 2; i < m; i++)
            {
                string tmp;
                tmp = Convert.ToString(i-1);
                tmp = "del" + tmp + "Y     ";
                diftableChar[0, i] = tmp;
            }


            for (int i=0;i<n+1;i++)
            {
                Console.WriteLine("\n");
                for (int j = 0; j < m; j++)
                {
                    
                    Console.Write(diftableChar[i,j]);
                }
            }
        }

        public void FillingMatrix()
        {
            int q = 1;
            for(int j=2;j<m;j++)
            {
                for(int i=0;i<n-q;i++)
                {

                    diftable[i, j] = diftable[i + 1, j - 1] - diftable[i, j - 1]; 

                }
                q++;
            }

           
        }



        public double Fact(double c)
        {
            if (c == 1)
            {
                return 1;
            }
            else
            { 
                return c * Fact(c - 1);

            }
        }

        public double FirstNewtonPolynom(double x)
        {
            int pointeri;
            double x0 = 0;
            double q;
          
            x0 = diftable[0, 0];
            pointeri = 0;


          
            q = (x-x0) / h;

            double tmp;
            
            int p = 0;
            double sum=0;
            double tmpq=q;
            
            for (int j = 2; j < 12; j++)
            {
                
                tmp = (diftable[pointeri,j] * tmpq) / Fact(j-1);
                
                p++;
                tmpq =tmpq*(q - p);
                sum = sum + tmp;
            }
            sum = sum + diftable[pointeri, 1];


            return sum;
            
        }

        public double SecondNewtonPolynom(double x)
        {
            int pointeri = 0;
            double xn=0;
            double q;

            xn = diftable[n - 1, 0];
            pointeri = n - 1;


            double tmp;
            q = (x - xn) / h;
            double tmpq = q;
            int p = 0;
            double sum = 0;
            int index = pointeri;

            for (int j =2;j<m-1; j++)
            {
                index -= 1;
                tmp = (diftable[index, j] * tmpq) / Fact(j-1);
                p++;

                tmpq = tmpq * (q + p);
                sum = sum + tmp;
            }
            sum = sum + diftable[pointeri, 1];
            return sum;
            
        }


        public void FirsNewton()
        {
            NewtonFirst = new double[4, 5];

            NewtonFirst[0, 0] = 0 + 0.2 * h;
            NewtonFirst[1, 0] = 0 - 0.2 * h;
            NewtonFirst[2, 0] = 0 + 0.5 * h;
            NewtonFirst[3, 0] = 0 - 0.5 * h;
           
            for (int i=0;i<4;i++)
            {

                NewtonFirst[i, 1] = Math.Tan(Math.Pow(NewtonFirst[i, 0], 3));
                NewtonFirst[i, 2] = FirstNewtonPolynom(NewtonFirst[i, 0]);
                NewtonFirst[i, 3] = Math.Abs(NewtonFirst[i, 1] - NewtonFirst[i, 2]);
                NewtonFirst[i, 4] = Math.Abs((NewtonFirst[i, 3] / NewtonFirst[i, 1]) * 100);
                   
                
            }

            Console.WriteLine("\n");
            Console.WriteLine("X               Истинное     Полученное      Абсолютная     Относительная");
            for (int i=0;i<4;i++)
            {
                Console.Write("\n");
                for(int j=0;j<5;j++)
                {
                    Console.Write("{0:f10}   ",NewtonFirst[i, j]);
                }
            }


        }



        public void SecondNewton()
        {
            NewtonSecond = new double[4, 5];

            NewtonSecond[0, 0] = 0.5 + 0.25 * h;
            NewtonSecond[1, 0] = 0.5 - 0.25 * h;
            NewtonSecond[2, 0] = 0.5 + 0.5 * h;
            NewtonSecond[3, 0] = 0.5 - 0.5 * h;
            for (int i = 0; i < 4; i++)
            {

                NewtonSecond[i, 1] = Math.Tan(Math.Pow(NewtonSecond[i, 0], 3));
                NewtonSecond[i, 2] = SecondNewtonPolynom(NewtonSecond[i, 0]);
                NewtonSecond[i, 3] = Math.Abs(NewtonSecond[i, 1] - NewtonSecond[i, 2]);
                NewtonSecond[i, 4] = Math.Abs((NewtonSecond[i, 3] / NewtonSecond[i, 1]) * 100);


            }

            Console.WriteLine("\n");
            Console.WriteLine("X                Истинное     Полученное      Абсолютная     Относительная");
            for (int i = 0; i < 4; i++)
            {
                Console.Write("\n");
                for (int j = 0; j < 5; j++)
                {
                    Console.Write("{0:f10}   " , NewtonSecond[i, j]);
                }
            }


        }
    }
}
