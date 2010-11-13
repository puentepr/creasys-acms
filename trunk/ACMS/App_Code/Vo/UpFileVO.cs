using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// ActivatyVo 的摘要描述
/// </summary>
namespace ACMS.VO
{
    public partial class UpFileVO : BaseVO
    {
        public UpFileVO()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        private string _name;
        private string _path;


        public string name { get { return _name; } set { _name = value; } }
        public string path { get { return _path; } set { _path = value; } }
    


    }


}
