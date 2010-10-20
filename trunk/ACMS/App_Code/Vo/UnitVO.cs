using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// ActivatyVo 的摘要描述
/// </summary>
namespace ACMS.VO
{
    public partial class UnitVO : BaseVO
    {
        public UnitVO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        private string _id;
        private string _name;

     
        public string id { get { return _id; } set { _id = value; } }
        public string name { get { return _name; } set { _name = value; } }
    


    }


}
