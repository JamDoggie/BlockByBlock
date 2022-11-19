using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TorchLite.game.entity
{
    public class Player : Entity
    {
        public Guid Uuid { get; set; }

        public string Username { get; set; }
    }
}
