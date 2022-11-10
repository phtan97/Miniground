using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniGround.API.Models
{
    [Serializable]
    public class Errors
    {
        public static readonly ErrorObject SUCCESS = new ErrorObject { Code = 0, Message = "Thành công" };
        public static readonly ErrorObject FAILED = new ErrorObject { Code = 1, Message = "Thất bại" };
    }
    [Serializable]
    public class ErrorObject
    {
        public int Code = 0;
        public string Message = "Thành công";
        public object Data;
        public ErrorObject() { }

        public ErrorObject(ErrorObject Obj)
        {
            this.Code = Obj.Code;
            this.Message = Obj.Message;
            this.Data = Obj.Data;
        }
        public ErrorObject Failed(string Message)
        {
            return SetCode(Errors.FAILED).SetData(Message);
        }

        public T GetData<T>()
        {
            return (T)Data;

        }

        public ErrorObject SetData(object Data)
        {
            this.Data = Data;
            return this;
        }

        public ErrorObject SetCode(ErrorObject Obj)
        {
            this.Code = Obj.Code;
            this.Message = Obj.Message;
            return this;
        }
        public ErrorObject SetCode(ErrorObject Obj, object Data)
        {
            SetCode(Obj).Data = Data;
            return this;
        }
    }
}
