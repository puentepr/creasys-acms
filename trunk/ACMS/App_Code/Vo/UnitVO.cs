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

        private int? _id;
        private string _name;
        private string _active;

        public int? id { get { return _id; } set { _id = value; } }
        public string name { get { return _name; } set { _name = value; } }
        public string active { get { return _active; } set { _active = value; } }

    }

}
