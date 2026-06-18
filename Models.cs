using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casino777
{
    public static class Balance
    {
        public static event Action OnCashChanged;
        private static long _cash;
        static public long Cash
        {
            get { return _cash; }
            set
            {
                _cash = value;
                OnCashChanged?.Invoke();
            }
        }
        static Balance() { _cash = 1000; }


    }

    
}
