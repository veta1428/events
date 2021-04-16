using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using static events.Publisher;

namespace events
{
    class Subscriber
    {
        //private Label labelN;
        //private Label labelM;
        //private Label labelDelannoy;
        //private MainWindow window;

        private React PrintToControl;
        private Action Resize;
        private Window window;

        //public Subscriber(Label labelN, Label labelM, Label labelDelannoy, MainWindow window)
        //{
        //    this.labelN = labelN;
        //    this.labelM = labelM;
        //    this.labelDelannoy = labelDelannoy;
        //    this.window = window;
        //}

        public Subscriber(React print, Action resize, Window window)
        {
            this.PrintToControl = print;
            this.Resize = resize;
            this.window = window;
        }

        public delegate void Notify();

        public event Notify EndCalculating;

        public void Print(int n, int m, int delannoy)
        {
            if (!window.Dispatcher.CheckAccess())
            {
                window.Dispatcher.BeginInvoke(new Publisher.React(Print), n, m, delannoy);
                return;
            }
            PrintToControl(n, m, delannoy);
            Resize();
        }

        public void EndWorking()
        {
            EndCalculating();
        }
    }
}
