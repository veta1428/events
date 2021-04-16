using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace events
{
    class Publisher
    {
        private int sleepingTime;

        public delegate void React(int n, int m, int delannoy);

        public event React DelannoyIsReady;

        public Publisher(int sleepingtimeMS)
        {
            SleepingTime = sleepingtimeMS;
        }

        bool shouldWork = true;

        private int SleepingTime
        {
            set
            {
                if (value < 0)
                {
                    throw new Exception("Sleeping time must be positive.");
                }
                sleepingTime = value;
            }
        }

        public void StartWorking()
        {
            shouldWork = true;
            while (shouldWork)
            {
                Random rand = new Random();
                int n = rand.Next(0, 20);
                int m = rand.Next(0, 20);

                int delannoy = DelannoyNumber2(n, m);
                
                DelannoyIsReady(n, m, delannoy);

                Thread.Sleep(sleepingTime);
            }
        }

        public void ShoulStopWorking()
        {
            shouldWork = false;
        }

        private int DelannoyNumber(int n, int m)
        {
            if (n == 0 || m == 0)
            {
                return 1;
            }
            else
            {
                return DelannoyNumber(n - 1, m) + DelannoyNumber(n - 1, m - 1) + DelannoyNumber(n, m - 1);
            }
        }

        private int DelannoyNumber2(int n, int m)
        {
            int result = 0;
            int min = Min (n, m);

            for (int i = 0; i <= min; i++)
            {
                result += (int) Math.Pow(2, i) * C(m, i) * C(m, i);
            }
            return result;
        }
        
        private int Min(int n, int m)
        {
            return n < m ? n : m;
        }

        private int C(int k, int n)
        {
            return Factorial(k) / (Factorial(k - n) * Factorial(n));
        }

        private int Factorial(int n)
        {           
            int result = 1;

            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
    }
}
