using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// ActivatyVo 的摘要描述
/// </summary>
namespace ACMS.VO
{
    public partial class RoleListVO : BaseVO
    {
        public RoleListVO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        private int? _id;
        private string _role_name;

        public int? id { get { return _id; } set { _id = value; } }
        public string role_name { get { return _role_name; } set { _role_name = value; } }

    }

}
