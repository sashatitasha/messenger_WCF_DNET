using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyChat
{
    // реализация всех методов интерфейса
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] // много клиентов - один и тот же сервер
    public class ServiceChat : IServiceChat
    {
        List<User> users = new List<User>();
        int next_user_id = 1;

        public int Connect(string username)
        {
            User server_user = new User(next_user_id, username, OperationContext.Current);

            next_user_id++;
            SendMessage(username + " подключился к чату!", 0);
            users.Add(server_user);
            return server_user.ID;
        }

        public void Disconnect(int client_id)
        {
            var user = users.FirstOrDefault(i => i.ID == client_id);

            if (user != null)
            {
                users.Remove(user);
                SendMessage(user.Username + " покинул чат :(", 0);
            }
        }

        public void SendMessage(string message, int user_id)
        {
            foreach (var usr in users)
            {
                string answer = DateTime.Now.ToShortTimeString();

                var user = users.FirstOrDefault(i => i.ID == user_id);

                if (user != null)
                {
                    answer += ";    " + user.Username;
                }

                answer += " " + message;

                usr.operationContext.GetCallbackChannel<IServiceChatCallBack>().MessageCallBack(answer);
            }
        }
    }
}
