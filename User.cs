using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace MyChat
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public OperationContext operationContext { get; set; }

        public User(int id, string name, OperationContext op)
        {
            ID = id;
            Username = name;
            operationContext = op;
        }
    }
}
