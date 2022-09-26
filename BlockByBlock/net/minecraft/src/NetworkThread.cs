using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace net.minecraft.src
{
    public abstract class NetworkThread
    {
        internal readonly NetworkManager netManager;

        internal readonly Thread thread;

        protected CancellationTokenSource tokenSource;

        public NetworkThread(NetworkManager networkManager, CancellationTokenSource tokenSource)
        {
            netManager = networkManager;
            thread = new Thread(() => runThreadLoop());
            this.tokenSource = tokenSource;
        }

        public virtual void startThread()
        {
            thread.Start();
        }

        protected virtual void runThreadLoop()
        {
            
        }

        public virtual void stopThread()
        {
            
        }
    }
}
