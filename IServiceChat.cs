using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MyChat
{
    [ServiceContract(CallbackContract = typeof(IServiceChatCallBack))] //договоренность как клиент взаимодействует с сервисом
    public interface IServiceChat
    {
        [OperationContract] //виден со стороны клиента
        int Connect(string username); //подключение к чату

        [OperationContract]
        void Disconnect(int client_id); //отключение от чата

        [OperationContract(IsOneWay = true)]
        void SendMessage(string message, int user_id); //отправка сообщения пользователяю
    }

    public interface IServiceChatCallBack
    {
        [OperationContract(IsOneWay = true)]
        void MessageCallBack(string message);
    }
}
